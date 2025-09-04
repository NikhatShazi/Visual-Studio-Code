using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eGift.Store.Razor.Pages.Orders
{
    public class DetailsModel : PageModel
    {
        private readonly eGift.Store.Razor.Data.eGiftStoreContext _context;

        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion


        #region Constructors
        public DetailsModel(IConfiguration configuration)
        {
            //_context = context;
            _configuration = configuration;
            this._webApiHelper = new WebApiHelper(configuration);
           // OrderViewModel = new OrderViewModel();
        }
        #endregion

        #region Properties
        [BindProperty]
        public OrderViewModel OrderViewModel { get; set; }

        [BindProperty]
        public List<OrderDetailsViewModel> OrderDetailsList { get; set; }
        #endregion

        #region MyRegion

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            OrderViewModel = new OrderViewModel();
            OrderDetailsList = new List<OrderDetailsViewModel>();


            //Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Order", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                OrderViewModel = JsonConvert.DeserializeObject<OrderViewModel>(response);

                //Webapiclient call
                var customerResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", OrderViewModel.CustomerId));
                if (!string.IsNullOrWhiteSpace(customerResponse))
                {
                    var customer = JsonConvert.DeserializeObject<CustomerViewModel>(customerResponse);
                    OrderViewModel.CustomerId = customer.ID;

                    OrderViewModel.CustomerName = customer.Name;
                    OrderViewModel.Address = customer.AddressName;
                    OrderViewModel.ContactNo = customer.Mobile;
                    OrderViewModel.StatusName = ((Status)OrderViewModel.StatusId).ToString();
                }

                //Webapiclient call
                var orderDetailsResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?orderId={2}", "OrderDetails", "GetByOrderId", id));
                if (!string.IsNullOrWhiteSpace(orderDetailsResponse))
                {
                    OrderDetailsList = JsonConvert.DeserializeObject<OrderDetailsViewModel[]>(orderDetailsResponse).ToList();
                }

                //Webapiclient call
                var productsResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Product"));
                if (!string.IsNullOrWhiteSpace(productsResponse))
                {
                    var productList = JsonConvert.DeserializeObject<ProductViewModel[]>(productsResponse).ToList();

                    //Join
                    OrderDetailsList = OrderDetailsList.Join(productList, od => od.ProductId, p => p.ID, (od, p) => new { od, p }).Select(m => { m.od.ProductName = m.p.Name; return m.od; }).ToList();
                }
            }
            return Page();
        }
        #endregion
    }
}
