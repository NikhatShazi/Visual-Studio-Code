using eGift.Admin.MVC.Common;
using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace eGift.Admin.MVC.Controllers
{
    public class EmployeeController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public EmployeeController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions 
        // GET: EmployeeController
        public ActionResult Index()
        {
            var employeeList = new EmployeeListViewModel();

            // Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Employee"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                employeeList.EmployeeList = JsonConvert.DeserializeObject<EmployeeViewModel[]>(response).ToList();
                employeeList.EmployeeList.ForEach(x => x.RoleName = ((RoleType)x.RoleId).GetDescription());
            }
            return View(employeeList);
        }

        // GET: EmployeeController/Details/5
        public ActionResult Details(int id)
        {
            var model = new EmployeeViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Employee", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                model.RoleName = ((RoleType)model.RoleId).GetDescription();
                model.GenderName = ((Gender)model.GenderId).ToString();
            }
            return View(model);
        }

        // GET: EmployeeController/Create
        public ActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        // POST: EmployeeController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel model)
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

                model.RoleId = (int)RoleType.Employee;

                //Model to json string
                var modelData = JsonConvert.SerializeObject(model);

                //Webapiclient call
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Employee"), modelData);

                //Response after create
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var employeeModel = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                    model.LoginModel.RefId = employeeModel.ID;
                    model.LoginModel.RefType = RoleType.Employee.ToString();
                    model.LoginModel.RoleId = employeeModel.RoleId;
                    model.LoginModel.IsActive = employeeModel.IsActive;

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

        // GET: EmployeeController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new EmployeeViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Employee", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                var loginResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}&type={3}", "Login", "GetByRefId", id, RoleType.Employee.ToString()));
                if (!string.IsNullOrWhiteSpace(loginResponse))
                {
                    model.LoginModel = JsonConvert.DeserializeObject<LoginViewModel>(loginResponse);

                }
            }
            return View(model);
        }

        // POST: EmployeeController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EmployeeViewModel model)
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

                model.RoleId = (int)RoleType.Employee;

                //Model to json string
                var modelData = JsonConvert.SerializeObject(model);

                //Webapiclient call
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Employee"), modelData);

                //Response after create
                if (!string.IsNullOrWhiteSpace(response))
                {
                    var employeeModel = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                    model.LoginModel.ID = -1;
                    model.LoginModel.RefId = employeeModel.ID;
                    model.LoginModel.RefType = RoleType.Employee.ToString();
                    model.LoginModel.RoleId = employeeModel.RoleId;
                    model.LoginModel.IsActive = employeeModel.IsActive;

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


        // GET: EmployeeController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new EmployeeViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Employee", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<EmployeeViewModel>(response);
                model.RoleName = ((RoleType)model.RoleId).GetDescription();
                model.GenderName = ((Gender)model.GenderId).ToString();
            }
            return View(model);
        }

        // POST: EmployeeController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, EmployeeViewModel model)
        {
            try
            {

                //web api call
                var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "Employee", id));
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
