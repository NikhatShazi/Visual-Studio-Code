using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eGift.Store.Razor.Data;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Net;
using eGift.Store.Razor.Helpers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace eGift.Store.Razor.Pages.Customer
{
    public class LoginModel : PageModel
    {
        #region Variables
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public LoginModel(IConfiguration configuration)
        {
            this._webApiHelper = new WebApiHelper(configuration);
            SigninViewModel = new SigninViewModel();
        }
        #endregion

        #region Properties
        [BindProperty]
        public SigninViewModel SigninViewModel { get; set; }
        #endregion

        #region Actions
        public void OnGet()
        {
            //return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Web API client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/Login/GetLoginCustomer?userName={0}&password={1}", SigninViewModel.UserName, SigninViewModel.Password));
            if (!string.IsNullOrWhiteSpace(response))
            {
                HttpContext.Session.SetString("UserName", SigninViewModel.UserName);

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
        #endregion

        #region Login Handler Methods
        public IActionResult OnGetLogout()
        {
            //User session.
            HttpContext.Session.SetString("UserName", "");
            HttpContext.Session.SetInt32("UserId", 0);
            HttpContext.Session.SetString("LoginDate", "");
            HttpContext.Session.SetString("LastLoginDate", "");

            //Order session
            HttpContext.Session.Remove("Order");
            HttpContext.Session.Remove("OrderDetails");
            HttpContext.Session.Remove("AddToCartList");
            HttpContext.Session.Remove("ProductList");


            return RedirectToPage("/Index");
        }
        #endregion
    }
}

