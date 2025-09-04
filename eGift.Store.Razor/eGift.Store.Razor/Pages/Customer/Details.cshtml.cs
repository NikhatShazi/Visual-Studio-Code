using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using eGift.Store.Razor.Data;
using eGift.Store.Razor.Models.ViewModels;
using eGift.Store.Razor.Helpers;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using eGift.Store.Razor.Common;

namespace eGift.Store.Razor.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly eGift.Store.Razor.Data.eGiftStoreContext _context;

        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion


        #region Constructors
        public DetailsModel(eGift.Store.Razor.Data.eGiftStoreContext context, IConfiguration configuration)
        {
            //_context = context;
            _configuration = configuration;
            this._webApiHelper = new WebApiHelper(configuration);
            CustomerViewModel = new CustomerViewModel();
        }
        #endregion

        #region Properties
        [BindProperty]
        public CustomerViewModel CustomerViewModel { get; set; }
        #endregion

        public void OnGet(int? id)
        {
            if (id != null)
            {
                //Web API client call
                var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", id));
                if (!string.IsNullOrWhiteSpace(response))
                {
                    CustomerViewModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                    CustomerViewModel.RoleName = ((RoleType)CustomerViewModel.RoleId).GetDescription();
                    CustomerViewModel.GenderName = ((Gender)CustomerViewModel.GenderId).ToString();
                }
                //if (CustomerViewModel == null)
                //{
                //    return NotFound();
                //}
            }
            
        }
    }
}
