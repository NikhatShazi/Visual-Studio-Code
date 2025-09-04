using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace eGift.Admin.MVC.Controllers
{
    public class CustomerController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions

        // GET: CustomerController
        public ActionResult Index()
        {
            var customerList = new CustomerListViewModel();

            // Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Customer"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                customerList.CustomerList = JsonConvert.DeserializeObject<CustomerViewModel[]>(response).ToList();
                customerList.CustomerList.ForEach(x => x.RoleName = ((RoleType)x.RoleId).GetDescription());
            }
            return View(customerList);
        }

        // GET: CustomerController/Details/5
        public ActionResult Details(int id)
        {
            {
                var model = new CustomerViewModel();

                //web api client call
                var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", id));
                if (!string.IsNullOrWhiteSpace(response))
                {
                    model = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                    model.RoleName = ((RoleType)model.RoleId).GetDescription();
                    model.GenderName = ((Gender)model.GenderId).ToString();
                }
                return View(model);
            }
        }

        // GET: CustomerController/Create
        public ActionResult Create()
        {
            return View(new CustomerViewModel());
        }

        // POST: CustomerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CustomerViewModel model)
        {
            try
            {
                // Server side validation.

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Save image in database.
                if (model.ProfileImage != null)
                {
                    if (model.ProfileImage.Length > 0)
                    {
                        // To save image data
                        using (var ms = new MemoryStream())
                        {
                            model.ProfileImage.CopyTo(ms);
                            model.ProfileImageData = ms.ToArray();
                        }

                        // To save file name.
                        model.ProfileImagePath = model.ProfileImage.FileName;
                        model.ProfileImage = null;
                    }
                }

                model.RoleId = (int)RoleType.Customer;

                //Model to json string
                var modelData = JsonConvert.SerializeObject(model);

                //Webapiclient call
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Customer"), modelData);

                //Response after create
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var customerModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                    model.LoginModel.RefId = customerModel.ID;
                    model.LoginModel.RefType = RoleType.Customer.ToString();
                    model.LoginModel.RoleId = customerModel.RoleId;
                    model.LoginModel.IsActive = customerModel.IsActive;

                    //Model to Json string.
                    var loginModelData = JsonConvert.SerializeObject(model.LoginModel);

                    // Web api client call
                    var loginResponse = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Login"), loginModelData);


                    if (!string.IsNullOrWhiteSpace(loginResponse))
                    {
                        //Redirect to List
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch
            {
            }
            return View(model);
        }


        // GET: CustomerController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CustomerViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                var loginResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}&type={3}", "Login", "GetByRefId", id, RoleType.Customer.ToString()));
                if (!string.IsNullOrWhiteSpace(loginResponse))
                {
                    model.LoginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);

                }
            }
            return View(model);
        }






        // POST: CustomerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerViewModel model)
        {
            try
            {
                // Server side validation.

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Save image in database.
                if (model.ProfileImage != null)
                {
                    if (model.ProfileImage.Length > 0)
                    {
                        // To save image data
                        using (var ms = new MemoryStream())
                        {
                            model.ProfileImage.CopyTo(ms);
                            model.ProfileImageData = ms.ToArray();
                        }

                        // To save file name.
                        model.ProfileImagePath = model.ProfileImage.FileName;
                        model.ProfileImage = null;
                    }
                }

                model.RoleId = (int)RoleType.Customer;

                //Model to json string
                var modelData = JsonConvert.SerializeObject(model);

                //Webapiclient call
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Customer"), modelData);

                //Response after create
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var customerModel = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                    model.LoginModel.ID = -1;
                    model.LoginModel.RefId = customerModel.ID;
                    model.LoginModel.RefType = RoleType.Customer.ToString();
                    model.LoginModel.RoleId = customerModel.RoleId;
                    model.LoginModel.IsActive = customerModel.IsActive;

                    //Model to Json string.
                    var loginModelData = JsonConvert.SerializeObject(model.LoginModel);

                    // Web api client call
                    var loginResponse = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Login"), loginModelData);


                    if (!string.IsNullOrWhiteSpace(loginResponse))
                    {
                        //Redirect to List
                        return RedirectToAction(nameof(Index));
                    }
                }

            }
            catch
            {
            }
            return View(model);

        }

        // GET: CustomerController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new CustomerViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Customer", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<CustomerViewModel>(response);
                model.RoleName = ((RoleType)model.RoleId).GetDescription();
                model.GenderName = ((Gender)model.GenderId).ToString();
            }
            return View(model);
        }

        // POST: CustomerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CustomerListViewModel model)
        {
            try
            {

                //web api call
                var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "Customer", id));
                //Response after delete
                if (!string.IsNullOrWhiteSpace(response))
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return View(model);
                }
            }
            catch
            {
                return View(model);
            }
        }
        #endregion
    }
}
