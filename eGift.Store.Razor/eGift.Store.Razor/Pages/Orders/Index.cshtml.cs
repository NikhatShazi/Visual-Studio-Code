using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace eGift.Store.Razor.Pages.Orders
{
    public class IndexModel : PageModel
    {
        #region Variables

        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;

        #endregion

        #region Constructors
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        public List<OrderViewModel> OrderList { get; set; }
        //public List<OrderDetailsViewModel> OrderDetailsModel { get;set; }
        public void OnGetAsync()
        {
            var loginUserId = HttpContext.Session.GetInt32("UserId");

            //Webapiclient call
            var ordersResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?loginUserId={2}", "Order", "MyOrder", loginUserId));
            if (!string.IsNullOrWhiteSpace(ordersResponse))
            {
                OrderList = JsonConvert.DeserializeObject<OrderViewModel[]>(ordersResponse).ToList();
            }
        }
    }
}
