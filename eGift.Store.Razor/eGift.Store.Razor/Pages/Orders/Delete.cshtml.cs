using eGift.Store.Razor.Common;
using eGift.Store.Razor.Helpers;
using eGift.Store.Razor.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System;

namespace eGift.Store.Razor.Pages.Orders
{
    public class DeleteModel : PageModel
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public DeleteModel(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Properties

        [BindProperty]
        public OrderViewModel OrderModel { get; set; }
        #endregion
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            //Webapiclient call
            var orderResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Order", id));
            if (!string.IsNullOrWhiteSpace(orderResponse))
            {
                OrderModel = JsonConvert.DeserializeObject<OrderViewModel>(orderResponse);

                // cancel date and statud id > 6
                OrderModel.CancelDate = DateTime.Now;
                OrderModel.StatusId = (int)Status.Cancelled;

                //convert model to json string
                var modelData = JsonConvert.SerializeObject(OrderModel);

                // Web client call
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Order"), modelData);
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToPage("./Index");
        }
    }
}
