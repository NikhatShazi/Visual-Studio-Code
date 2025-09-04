using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using eGift.Store.Razor.Data;
using eGift.Store.Razor.Models.ViewModels;
using eGift.Store.Razor.Helpers;
using Microsoft.Extensions.Configuration;
using System.IO;
using eGift.Store.Razor.Common;
using Newtonsoft.Json;

namespace eGift.Store.Razor.Pages.Customer
{
    public class CreateModel : PageModel
    {
        #region Variables
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public CreateModel( IConfiguration configuration)
        {
            this._webApiHelper = new WebApiHelper(configuration);
            CustomerViewModel = new CustomerViewModel();
        }
        #endregion

        #region Properties
        [BindProperty]
        public CustomerViewModel CustomerViewModel { get; set; }
        #endregion

        #region Actions
        public IActionResult OnGet()
        {
            return Page();
        }
       
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //Save image in database
            if (CustomerViewModel.ProfileImage != null)
            {
                if (CustomerViewModel.ProfileImage.Length > 0)
                {
                    //To save image data
                    using (var ms = new MemoryStream())
                    {
                        CustomerViewModel.ProfileImage.CopyTo(ms);
                        CustomerViewModel.ProfileImageData = ms.ToArray();

                    }

                    //To save file name
                    CustomerViewModel.ProfileImagePath = CustomerViewModel.ProfileImage.FileName;
                    CustomerViewModel.ProfileImage = null;
                }
            }
            CustomerViewModel.RoleId = (int)RoleType.Customer;

            //model to json string
            var modelData = JsonConvert.SerializeObject(CustomerViewModel);

            //Webapiclient call
            var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Customer"), modelData);

            //Response after create
            if (!string.IsNullOrWhiteSpace(response))
            {
                var customerModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);

                CustomerViewModel.LoginModel.RefId = customerModel.ID;
                CustomerViewModel.LoginModel.RefType = RoleType.Customer.ToString();
                CustomerViewModel.LoginModel.RoleId = customerModel.RoleId;
                CustomerViewModel.LoginModel.IsActive = customerModel.IsActive;

                //Model to json string
                var loginModelData = JsonConvert.SerializeObject(CustomerViewModel.LoginModel);

                //Web api client call
                var loginResponse = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Login"), loginModelData);

                if (!string.IsNullOrWhiteSpace(loginResponse))
                {
                    //Redirect to list
                    return RedirectToPage("/Index");
                }
            }
            return RedirectToPage("./Index");
        }
        #endregion
    }
}
