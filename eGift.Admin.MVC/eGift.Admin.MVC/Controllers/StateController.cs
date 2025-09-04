using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace eGift.Admin.MVC.Controllers
{
    public class StateController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public StateController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions
        // GET: StateController
        public ActionResult Index()
        {
            var stateList = new StateListViewModel();

            // Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "State"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                stateList.StateList = JsonConvert.DeserializeObject<StateViewModel[]>(response).ToList();
            }

            //Web APi client call.

            var countryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Country", "GetAll"));
            if (!string.IsNullOrWhiteSpace(countryResponse))
            {
                //List response
                var countryList = JsonConvert.DeserializeObject<CountryViewModel[]>(countryResponse).ToList();
                stateList.StateList = stateList.StateList.Join(countryList, s => s.CountryId, c => c.ID, (s, c) => new { s, c }).Select(m => { m.s.CountryName = m.c.CountryName; return m.s; }).ToList();

            }
            return View(stateList);
        }

        // GET: StateController/Details/5
        public ActionResult Details(int id)
        {
            var model = new StateViewModel();

            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "State", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<StateViewModel>(response);
                var countryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "Country", "GetFromDelete", model.CountryId));
                if (!string.IsNullOrWhiteSpace(countryResponse))
                {
                    var countryModel = JsonConvert.DeserializeObject<CountryViewModel>(countryResponse);
                    model.CountryName = countryModel.CountryName;
                }
            }
            return View(model);
        }

        // GET: StateController/Create
        public ActionResult Create()
        {
            var model = new StateViewModel();
            GetAllList(model);
            return View(model);
        }


        // POST: StateController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StateViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "State"), modelData);

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

        // GET: StateController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new StateViewModel();
            // Web API client call.
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "State", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<StateViewModel>(response);
                GetAllList(model);
            }
            return View(model);
        }

        // POST: StateController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, StateViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "State"), modelData);

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

        // GET: StateController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new StateViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "State", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<StateViewModel>(response);
                var countryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "Country", "GetFromDelete", model.CountryId));
                if (!string.IsNullOrWhiteSpace(countryResponse))
                {
                    var countryModel = JsonConvert.DeserializeObject<CountryViewModel>(countryResponse);
                    model.CountryName = countryModel.CountryName;
                }
            }
            return View(model);
        }

        // POST: StateController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, StateViewModel model)
        {
            // web api call
            var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "State", id));
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

        private void GetAllList(StateViewModel model)
        {
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Country", "GetAll"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                var countryList = JsonConvert.DeserializeObject<CountryViewModel[]>(response).ToList();
                model.CountryList = new SelectList(countryList, "ID", "CountryName");

            }
        }
        #endregion
    }
}
