using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace eGift.Admin.MVC.Controllers
{
    public class AddressController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public AddressController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions


        // GET: AddressController
        public ActionResult Index()
        {
            var addresses = new AddressListViewModel();

            // Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Address"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                addresses.AddressList = JsonConvert.DeserializeObject<AddressViewModel[]>(response).ToList();
            }

            //Web APi client call.
            var cityResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "City", "GetAll"));
            if (!string.IsNullOrWhiteSpace(cityResponse))
            {
                //List response
                var cityList = JsonConvert.DeserializeObject<CityViewModel[]>(cityResponse).ToList();
                addresses.AddressList = addresses.AddressList.Join(cityList, a => a.CityId, c => c.ID, (a, c) => new { a, c }).Select(m => { m.a.CityName = m.c.CityName; return m.a; }).ToList();

            }
            //Web APi client call.
            var stateResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "State", "GetAll"));
            if (!string.IsNullOrWhiteSpace(stateResponse))
            {
                //List response
                var stateList = JsonConvert.DeserializeObject<StateViewModel[]>(stateResponse).ToList();
                addresses.AddressList = addresses.AddressList.Join(stateList, a => a.StateId, s => s.ID, (a, s) => new { a, s }).Select(m => { m.a.StateName = m.s.StateName; return m.a; }).ToList();

            }

            //Web APi client call.
            var countryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Country", "GetAll"));
            if (!string.IsNullOrWhiteSpace(countryResponse))
            {
                //List response
                //var stateList = new StateListViewModel();
                var countryList = JsonConvert.DeserializeObject<CountryViewModel[]>(countryResponse).ToList();
                addresses.AddressList = addresses.AddressList.Join(countryList, a => a.CountryId, c => c.ID, (a, c) => new { a, c }).Select(m => { m.a.CountryName = m.c.CountryName; return m.a; }).ToList();
            }

            return View(addresses);
        }

        // GET: AddressController/Details/5
        public ActionResult Details(int id)
        {
            var model = new AddressViewModel();
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Address", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<AddressViewModel>(response);
                GetCountryStateCityList(model);
            }
            return View(model);
        }

        // GET: AddressController/Create
        public ActionResult Create()
        {
            var model = new AddressViewModel();

            GetAllList(model);
            return View(model);
        }

        // POST: AddressController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddressViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Address"), modelData);

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

        //private ActionResult View(object model)
        //{
        //    throw new NotImplementedException();
        //}

        // GET: AddressController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new AddressViewModel();
            // Web API client call.
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Address", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<AddressViewModel>(response);
                GetAllList(model);
            }
            return View(model);
        }

        // POST: AddressController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, AddressViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Address"), modelData);

                // Response after create.
                if (!string.IsNullOrWhiteSpace(response))
                {
                    //Redirect to list.
                    return RedirectToAction(nameof(Index));
                }
            }
            catch
            {
                //return View();
            }
            return View(model);
        }

        // GET: AddressController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new AddressViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Address", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<AddressViewModel>(response);
                GetCountryStateCityList(model);


            }

            return View(model);
        }

        // POST: AddressController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, AddressViewModel model)
        {
            // web api call
            var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "Address", id));
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
        #endregion




        #region Private Functions
        private void GetAllList(AddressViewModel model)
        {
            //Web client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Country"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //list response
                var countryList = JsonConvert.DeserializeObject<CountryViewModel[]>(response).ToList();
                model.CountryList = new SelectList(countryList, "ID", "CountryName");
            }
        }       

        private void GetCountryStateCityList(AddressViewModel model)
        {
            var cityResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "City", "GetFromDelete", model.CityId));
            if (!string.IsNullOrWhiteSpace(cityResponse))
            {
                var cityModel = JsonConvert.DeserializeObject<CityViewModel>(cityResponse);
                model.CityName = cityModel.CityName;
            }

            var stateResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "State", "GetFromDelete", model.StateId));
            if (!string.IsNullOrWhiteSpace(stateResponse))
            {
                var stateModel = JsonConvert.DeserializeObject<StateViewModel>(stateResponse);
                model.StateName = stateModel.StateName;
            }

            var countryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "Country", "GetFromDelete", model.CountryId));
            if (!string.IsNullOrWhiteSpace(countryResponse))
            {
                var countryModel = JsonConvert.DeserializeObject<CountryViewModel>(countryResponse);
                model.CountryName = countryModel.CountryName;
            }
        }
        #endregion

        #region Ajax Actions
        public JsonResult GetStatesByCountry(int countryId)
        {
            //webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?countryId={2}", "State", "GetByCountryId", countryId));
            if (!string.IsNullOrWhiteSpace(response))
            {
                return Json(response);
            }
            return Json(false);
        }

        public JsonResult GetCitiesByState(int stateId)
        {
            //webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?stateId={2}", "City", "GetByStateId", stateId));
            if (!string.IsNullOrWhiteSpace(response))
            {
                return Json(response);
            }
            return Json(false);
        }
        #endregion
    }
}
