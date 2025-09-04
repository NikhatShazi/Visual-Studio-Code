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
    public class CityController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public CityController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions

        // GET: CityController
        public ActionResult Index()
        {
            var cityList = new CityListViewModel();

            // Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "City"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                cityList.CityList = JsonConvert.DeserializeObject<CityViewModel[]>(response).ToList();
            }

            //Web APi client call.
            var stateResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "State", "GetAll"));
            if (!string.IsNullOrWhiteSpace(stateResponse))
            {
                //List response
                var stateList = JsonConvert.DeserializeObject<StateViewModel[]>(stateResponse).ToList();
                cityList.CityList = cityList.CityList.Join(stateList, s => s.StateId, c => c.ID, (s, c) => new { s, c }).Select(m => { m.s.StateName = m.c.StateName; return m.s; }).ToList();

            }
            return View(cityList);
        }

        // GET: CityController/Details/5
        public ActionResult Details(int id)
        {
            var model = new CityViewModel();

            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "City", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<CityViewModel>(response);
                var stateResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "State", "GetFromDelete", model.StateId));
                if (!string.IsNullOrWhiteSpace(stateResponse))
                {
                    var stateModel = JsonConvert.DeserializeObject<CityViewModel>(stateResponse);
                    model.StateName = stateModel.StateName;
                }
            }
            return View(model);
        }

        // GET: CityController/Create
        public ActionResult Create()
        {
            //return View(new CityViewModel());
            var model = new CityViewModel();
            
            GetAllList(model);
            return View(model);
        }

        //private void GetAllList(object model)
        //{
        //    throw new NotImplementedException();
        //}

        // POST: CityController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CityViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "City"), modelData);

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

        // GET: CityController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CityViewModel();
            // Web API client call.
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "City", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<CityViewModel>(response);
                GetAllList(model);
            }
            return View(model);
        }

        // POST: CityController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CityViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "City"), modelData);

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

        // GET: CityController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new CityViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "City", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<CityViewModel>(response);
                var stateResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "State", "GetFromDelete", model.StateId));
                if (!string.IsNullOrWhiteSpace(stateResponse))
                {
                    var stateModel = JsonConvert.DeserializeObject<StateViewModel>(stateResponse);
                    model.StateName = stateModel.StateName;
                }
            }
            return View(model);
        }

        // POST: CityController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CityViewModel model)
        {
            // web api call
            var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "City", id));
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

        private void GetAllList(CityViewModel model)
        {
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "State", "GetAll"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                var stateList = JsonConvert.DeserializeObject<StateViewModel[]>(response).ToList();
                model.StateList = new SelectList(stateList, "ID", "StateName");

            }
        }
        #endregion
    }
}
