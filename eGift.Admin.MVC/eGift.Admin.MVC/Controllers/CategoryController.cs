using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Linq;

namespace eGift.Admin.MVC.Controllers
{
    public class CategoryController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }

        #endregion

        #region Default CRUD Actions


        // GET: CategoryController
        public ActionResult Index()
        {
            var categoryList = new CategoryListViewModel();

            //Web API client call.
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Category"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response.
                categoryList.CategoryList = JsonConvert.DeserializeObject<CategoryViewModel[]>(response).ToList();
            }
            return View(categoryList);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var model = new CategoryViewModel();

            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Category", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<CategoryViewModel>(response);
            }
            return View(model);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View(new CategoryViewModel());
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Category"), modelData);

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

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new CategoryViewModel();

            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Category", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<CategoryViewModel>(response);
            }
            return View(model);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryViewModel model)
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
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Category"), modelData);

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

        // GET: CategoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new CategoryViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Category", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<CategoryViewModel>(response);
            }
            return View(model);
        }

        // POST: CategoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryViewModel model)
        {
            // web api call
            var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "Category", id));
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
    }
}
