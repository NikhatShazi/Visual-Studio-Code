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
    public class CountryController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public CountryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions
        // GET: CountryController
        public ActionResult Index()
        {
            var countryList = new CountryListViewModel();

            // Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Country"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                countryList.CountryList = JsonConvert.DeserializeObject<CountryViewModel[]>(response).ToList();
            }
            return View(countryList);
        }

        // GET: CountryController/Details/5
        public ActionResult Details(int id)
        {
            var model = new CountryViewModel();

            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Country", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<CountryViewModel>(response);
            }
            return View(model);
        }

        // GET: CountryController/Create
        public ActionResult Create()
        {
            return View(new CountryViewModel());
        }

        // POST: CountryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CountryViewModel model)
        {
            try
            {
                // Server side validation.
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web API client call.
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Country"), modelData);

                // Response after create.
                if (!string.IsNullOrWhiteSpace(response))
                {
                    //Redirect to list.
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
            }
            return View(model);

        }

        // GET: CountryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CountryViewModel();

            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Country", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<CountryViewModel>(response);
            }
            return View(model);
        }

        // POST: CountryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CountryViewModel model)
        {
            try
            {
                // Server side validation.
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web API client call.
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Country"), modelData);

                // Response after create.
                if (!string.IsNullOrWhiteSpace(response))
                {
                    //Redirect to list.
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                return View();
            }
            return View(model);
        }

        // GET: CountryController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new CountryViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Country", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<CountryViewModel>(response);
            }
            return View(model);

        }
        // POST: CountryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CountryViewModel model)
        {
            //web api call
            var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "Country", id));
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

    }
}


#endregion
