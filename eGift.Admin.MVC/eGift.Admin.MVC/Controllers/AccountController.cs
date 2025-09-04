using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;

namespace eGift.Admin.MVC.Controllers
{
    public class AccountController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public AccountController(IConfiguration configuration)
        {
            this._configuration = configuration;
            this._webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Account Actions
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(SigninViewModel model)        
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            //Web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/Login/GetLoginEmployee?userName={0}&password={1}", model.UserName, model.Password));
            if (!string.IsNullOrWhiteSpace(response))
            {
                HttpContext.Session.SetString("UserName", model.UserName);
                var loginModel = JsonConvert.DeserializeObject<LoginViewModel>(response);
                loginModel.LoginDate = DateTime.Now;
                var modelData = JsonConvert.SerializeObject(loginModel);
                var saveLoginResponse = _webApiHelper.WebApiClientPost(string.Format("api/Login/SaveLoginDateTime"), modelData);
                if (!string.IsNullOrWhiteSpace(saveLoginResponse))
                {
                    var savedLoginModel = JsonConvert.DeserializeObject<LoginViewModel>(saveLoginResponse);
                    HttpContext.Session.SetString("LoginDate", savedLoginModel.LoginDate.ToString());
                    HttpContext.Session.SetString("LastLoginDate", savedLoginModel.LastLoginDate.ToString());
                }

                //Redirect to list
                return RedirectToAction("Index", "Home");
            }

            return View("Index", model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("UserName", "");
            return RedirectToAction("Index", "Account");

        }
        #endregion
    }
}
