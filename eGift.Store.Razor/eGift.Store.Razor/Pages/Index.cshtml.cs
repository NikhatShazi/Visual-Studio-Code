using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGift.Store.Razor.Pages
{
    public class IndexModel : PageModel
    {
        #region Variables
        private readonly ILogger<IndexModel> _logger;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Properties
        public List<ProductViewModel> ProductList { get; set; }
        [BindProperty]
        public SigninViewModel SigninViewModel { get; set; }
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; }
        #endregion

        #region Constructors
        public IndexModel(ILogger<IndexModel> logger, IConfiguration configuration)
        {
            this._logger = logger;
            this._webApiHelper = new WebApiHelper(configuration);
            SigninViewModel = new SigninViewModel();
            LoginViewModel = new LoginViewModel();
        }
        #endregion

        #region Home Page Method
        public void OnGet()
        {
            List<ProductViewModel> productList = HttpContext.Session.GetComplexData<List<ProductViewModel>>("ProductList");
            if (productList == null)
            {
                //Web api client call
                var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Product"));
                if (!string.IsNullOrWhiteSpace(response))
                {
                    ProductList = JsonConvert.DeserializeObject<ProductViewModel[]>(response).ToList();
                    HttpContext.Session.SetComplexData("ProductList", ProductList);
                }
            }
            else
            {
                ProductList = productList;
            }
            
        }
        #endregion

        #region Ajax actions
        public IActionResult OnGetGetLogin(string userName, string password)
        {
            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/Login/GetLoginCustomer?userName={0}&password={1}", userName, password));
            if (!string.IsNullOrWhiteSpace(response))
            {
                HttpContext.Session.SetString("UserName", userName);
                var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(response);
                loginModel.LoginDate = DateTime.Now;
                var modelData = JsonConvert.SerializeObject(loginModel);
                var saveLoginResponse = _webApiHelper.WebApiClientPost(string.Format("api/Login/SaveLoginDateTime"), modelData);
                if (!string.IsNullOrWhiteSpace(saveLoginResponse))
                {
                    var savedLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(saveLoginResponse);
                    HttpContext.Session.SetInt32("UserId", savedLoginModel.RefId);
                    HttpContext.Session.SetString("LoginDate", savedLoginModel.LoginDate.ToString());
                    HttpContext.Session.SetString("LastLoginDate", savedLoginModel.LastLoginDate.ToString());

                }
                //Redirect to list
                return RedirectToPage("/Index");
            }
            return Page();
        }

        public IActionResult OnGetAddToCartAsync(int productId, int quantity)
        {
            bool result = false;
            var userName = HttpContext.Session.GetString("UserName");
            if (!string.IsNullOrWhiteSpace(userName))
            {
                List<AddToCartModel> addToCartList = HttpContext.Session.GetComplexData<List<AddToCartModel>>("AddToCartList");
                if (addToCartList == null)
                {
                    addToCartList = new List<AddToCartModel>();
                }
                if (addToCartList.Any(x => x.Id == productId))
                {
                    // product already exist.
                    addToCartList.Where(x => x.Id == productId).ToList().ForEach(w => w.Quantity = quantity);
                }
                else
                {
                    // Product not exist
                    var addToCart = new AddToCartModel();
                    addToCart.Id = productId;
                    addToCart.Quantity = quantity;
                    addToCartList.Add(addToCart);
                }
                HttpContext.Session.SetComplexData("AddToCartList", addToCartList);

                List<ProductViewModel> productList = HttpContext.Session.GetComplexData<List<ProductViewModel>>("ProductList");
                productList.Where(x => x.ID == productId).ToList().ForEach(p => p.QuantityPerUnit = quantity);
                HttpContext.Session.SetComplexData("ProductList", productList);

                result = true;
            }
            else
            {
                result = false;
            }
            return new JsonResult(result);
        }
        #endregion
    }
}

