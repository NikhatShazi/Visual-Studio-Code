using eGift.Admin.MVC.Helpers;
using eGift.Admin.MVC.Models.ListViewModels;
using eGift.Admin.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using eGift.Admin.MVC.Common;

namespace eGift.Admin.MVC.Controllers
{
    public class ProductController : Controller
    {
        #region Variables
        public IConfiguration _configuration;
        public WebApiHelper _webApiHelper;
        #endregion

        #region Constructors
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _webApiHelper = new WebApiHelper(configuration);
        }
        #endregion

        #region Default CRUD Actions


        // GET: ProductController
        public ActionResult Index()
        
        {
            var list = new ProductListViewModel();

            //Webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Product"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //list response
                list.ProductList = JsonConvert.DeserializeObject<ProductViewModel[]>(response).ToList();
                list.ProductList.ForEach(x => x.SizeName = ((Size)x.SizeID).GetDescription());
                //Webapiclient call
                var categoryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Category", "GetAll"));
                if (!string.IsNullOrWhiteSpace(categoryResponse))
                {
                    //list response
                    var categoryList = JsonConvert.DeserializeObject<CategoryViewModel[]>(categoryResponse).ToList();
                    list.ProductList = list.ProductList.Join(categoryList, s => s.CategoryId, c => c.ID, (s, c) => new { s, c }).Select(m => { m.s.CategoryName = m.c.CategoryName; return m.s; }).ToList();
                }

                var subCategoryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "SubCategory", "GetAll"));
                if (!string.IsNullOrWhiteSpace(subCategoryResponse))
                {
                    //list response
                    var subCategoryList = JsonConvert.DeserializeObject<SubCategoryViewMode[]>(subCategoryResponse).ToList();
                    list.ProductList = list.ProductList.Join(subCategoryList, c => c.SubCategoryId, s => s.ID, (c, s) => new { c, s }).Select(m => { m.c.SubCategoryName = m.s.SubCategoryName; return m.c; }).ToList();
                }
            }
            return View(list);
        }

        // GET: ProductController/Details/5
        public ActionResult Details(int id)
        {
            var model = new ProductViewModel();
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Product", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<ProductViewModel>(response);
                GetCategorySubCategoryList(model);
            }
            return View(model);
        }

        // GET: ProductController/Create
        public ActionResult Create()
        {
            var model = new ProductViewModel();

            GetAllList(model);
            return View(model);
        }

        // POST: ProductController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            try
            {
                // Server side validation.
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Save image in database
                if (model.ImageUrl != null)
                {
                    if (model.ImageUrl.Length > 0)
                    {
                        //To save image data
                        using (var ms = new MemoryStream())
                        {
                            model.ImageUrl.CopyTo(ms);
                            model.ProductImageData = ms.ToArray();
                        }

                        //To save file name
                        model.ProductImagePath = model.ImageUrl.FileName;
                        model.ImageUrl = null;
                    }
                }
                SaveAllPictures(model);
                //model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web API client call.
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Product"), modelData);

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

        // GET: ProductController/Edit/5
        public ActionResult Edit(int id)
        {
            var model = new ProductViewModel();
            // Web API client call.
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Product", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //List response
                model = JsonConvert.DeserializeObject<ProductViewModel>(response);
                GetAllList(model);
            }
            return View(model);
        }

        // POST: ProductController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ProductViewModel model)
        {
            try
            {
                // Server side validation.
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                //Save image in database
                if (model.ImageUrl != null)
                {
                    if (model.ImageUrl.Length > 0)
                    {
                        //To save image data
                        using (var ms = new MemoryStream())
                        {
                            model.ImageUrl.CopyTo(ms);
                            model.ProductImageData = ms.ToArray();
                        }

                        //To save file name
                        model.ProductImagePath = model.ImageUrl.FileName;
                        model.ImageUrl = null;
                    }
                }
                SaveAllPictures(model);
                //model to json string
                var modelData = JsonConvert.SerializeObject(model);

                // Web API client call.
                var response = _webApiHelper.WebApiClientPost(string.Format("api/{0}", "Product"), modelData);

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


        // GET: ProductController/Delete/5
        public ActionResult Delete(int id)
        {
            var model = new ProductViewModel();

            //web api client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}", "Product", id));
            if (!string.IsNullOrWhiteSpace(response))
            {
                model = JsonConvert.DeserializeObject<ProductViewModel>(response);
                GetCategorySubCategoryList(model);


            }

            return View(model);
        }

        // POST: ProductController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ProductViewModel model)
        {
            /// web api call
            var response = _webApiHelper.WebApiClientDelete(string.Format("api/{0}/{1}", "Product", id));
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
        private void GetAllList(ProductViewModel model)
        {
            //Web client call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}", "Category"));
            if (!string.IsNullOrWhiteSpace(response))
            {
                //list response
                var categoryList = JsonConvert.DeserializeObject<CategoryViewModel[]>(response).ToList();
                model.CategoryList = new SelectList(categoryList, "ID", "CategoryName");
            }
        }

        private void SaveAllPictures(ProductViewModel model)
        {
            if (model.Picture1 != null)
            {
                if (model.Picture1.Length > 0)
                {
                    //To save image data
                    using (var ms = new MemoryStream())
                    {
                        model.Picture1.CopyTo(ms);
                        model.PictureData1= ms.ToArray();
                    }

                    //To save file name
                    model.PicturePath1 = model.Picture1.FileName;
                    model.Picture1 = null;
                }
            }

            if (model.Picture2 != null)
            {
                if (model.Picture2.Length > 0)
                {
                    //To save image data
                    using (var ms = new MemoryStream())
                    {
                        model.Picture2.CopyTo(ms);
                        model.PictureData2 = ms.ToArray();
                    }

                    //To save file name
                    model.PicturePath2 = model.Picture2.FileName;
                    model.Picture2 = null;
                }
            }


            if (model.Picture3 != null)
            {
                if (model.Picture3.Length > 0)
                {
                    //To save image data
                    using (var ms = new MemoryStream())
                    {
                        model.Picture3.CopyTo(ms);
                        model.PictureData3 = ms.ToArray();
                    }

                    //To save file name
                    model.PicturePath3 = model.Picture3.FileName;
                    model.Picture3 = null;
                }
            }

            if (model.Picture4 != null)
            {
                if (model.Picture1.Length > 0)
                {
                    //To save image data
                    using (var ms = new MemoryStream())
                    {
                        model.Picture4.CopyTo(ms);
                        model.PictureData4 = ms.ToArray();
                    }

                    //To save file name
                    model.PicturePath4 = model.Picture1.FileName;
                    model.Picture4 = null;
                }
            }
        }

        private void GetCategorySubCategoryList(ProductViewModel model)
        {

            var categoryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "Category", "GetFromDelete", model.CategoryId));
            if (!string.IsNullOrWhiteSpace(categoryResponse))
            {
                var categoryModel = JsonConvert.DeserializeObject<CategoryViewModel>(categoryResponse);
                model.CategoryName = categoryModel.CategoryName;
            }
            var subCategoryResponse = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?id={2}", "SubCategory", "GetFromDelete", model.SubCategoryId));
            if (!string.IsNullOrWhiteSpace(subCategoryResponse))
            {
                var subCategoryModel = JsonConvert.DeserializeObject<SubCategoryViewMode>(subCategoryResponse);
                model.SubCategoryName = subCategoryModel.SubCategoryName;
            }
        }


        #endregion

        #region Ajax Actions
        public JsonResult GetSubCategoryByCategory(int categoryId)
        {
            //webapiclient call
            var response = _webApiHelper.WebApiClientGet(string.Format("api/{0}/{1}?categoryId={2}", "SubCategory", "GetByCategoryId", categoryId));
            if (!string.IsNullOrWhiteSpace(response))
            {
                return Json(response);
            }
            return Json(false);
        }


        #endregion
    }
}
