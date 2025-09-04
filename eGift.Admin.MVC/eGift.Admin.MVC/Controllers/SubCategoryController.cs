using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Drawing;
using System.Linq;

namespace eGift.Admin.MVC.Controllers
{
    public class SubCategoryController : Controller
    {
        #region Variables
        public IConfiguration _Configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public SubCategoryController(IConfiguration configuration)
        {
            _Configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions
        // GET: SubCategoryController
        public ActionResult Index()
        {
            var subCategoryList= new SubCategoryListViewModel();

            // Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "SubCategory"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                subCategoryList.SubCategoryList = JsonConvert.DeserializeObject<SubCategoryViewMode[]>(response).ToList();
            }

            //Web APi client call.

            var categoryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Category", "GetAll"));
            if (!string.IsNullOrWhiteSpace(categoryResponse))
            {
                //List response
                var categoryList = JsonConvert.DeserializeObject<CategoryViewModel[]>(categoryResponse).ToList();
                subCategoryList.SubCategoryList = subCategoryList.SubCategoryList.Join(categoryList, s => s.CategoryId, c => c.ID, (s, c) => new { s, c }).Select(m => { m.s.CategoryName = m.c.CategoryName; return m.s; }).ToList();
                
            }
            return View(subCategoryList);
        }

        // GET: SubCategoryController/Details/5
        public ActionResult Details(int id)
        {
            var model = new SubCategoryViewMode();

            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "SubCategory", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<SubCategoryViewMode>(response);
                var categoryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "Category", "GetFromDelete", model.CategoryId));
                if (!string.IsNullOrWhiteSpace(categoryResponse))
                {
                    var categoryModel = JsonConvert.DeserializeObject<CategoryViewModel>(categoryResponse);
                    model.CategoryName = categoryModel.CategoryName;
                }
            }
            return View(model);
        }

        // GET: SubCategoryController/Create
        public ActionResult Create()
        {
            var model = new SubCategoryViewMode();
           GetAllList(model);
            return View(model);
        }

        // POST: SubCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubCategoryViewMode model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "SubCategory"), modelData);

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

        // GET: SubCategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new SubCategoryViewMode();
            // Web API client call.
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "SubCategory", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<SubCategoryViewMode>(response);
                GetAllList(model);
            }
            return View(model);
        }

        // POST: SubCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SubCategoryViewMode model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "SubCategory"), modelData);

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

        // GET: SubCategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new SubCategoryViewMode();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "SubCategory", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<SubCategoryViewMode>(response);
                var categoryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "Category", "GetFromDelete", model.CategoryId));
                if (!string.IsNullOrWhiteSpace(categoryResponse))
                {
                    var categoryModel = JsonConvert.DeserializeObject<CategoryViewModel>(categoryResponse);
                    model.CategoryName = categoryModel.CategoryName;
                }
            }
            return View(model);
        }

        // POST: SubCategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, SubCategoryViewMode model)
        {

            // web api call
            var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "SubCategory", id));
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

        private void GetAllList(SubCategoryViewMode model)
        {
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Category", "GetAll"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                var categoryList = JsonConvert.DeserializeObject<CategoryViewModel[]>(response).ToList();
                model.CategoryList = new SelectList(categoryList, "ID", "CategoryName");

            }
        }
        #endregion
    }
}
