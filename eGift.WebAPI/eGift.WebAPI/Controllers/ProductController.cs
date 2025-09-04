using eGift.WebAPI.Models.DataModels;
using eGift.WebAPI.Models.DBContexts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public ProductController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Default CRUD Actions


        // GET: api/<ProductController>
        [HttpGet]
        public IActionResult Get()
        {
            var productList = _dbContext.ProductModel.Where(x => !x.IsDeleted).ToList();
            var categoryList = _dbContext.CategoryModel.ToList();
            var subCategoryList = _dbContext.SubCategoryModel.ToList();
            if (categoryList != null)       
            {
                productList = productList.Join(categoryList, s => s.CategoryId, c => c.ID, (s, c) => new { s, c }).Select(m => { m.s.CategoryName = m.c.CategoryName; return m.s; }).ToList();
            }
            if (subCategoryList != null)
            {
                productList = productList.Join(subCategoryList, c => c.SubCategoryId, s => s.ID, (c, s) => new { c, s }).Select(m => { m.c.SubCategoryName = m.s.SubCategoryName; return m.c; }).ToList();
            }

            return Ok(productList);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
            public IActionResult Get(int id)
            {
                var product = _dbContext.ProductModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
                return Ok(product);
            }

        //GET: api/CityController
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var prpductList = _dbContext.AddressModel.ToList();
            return Ok(prpductList);
        }

        // POST api/<ProductController>
        [HttpPost]
        public IActionResult Post(ProductModel model)
        {
            if (model.ID == 0)
            {
                model.IsDeleted = false;
                model.CreatedBy = 1; // Need to change
                model.CreatedDate = DateTime.Now;
                _dbContext.ProductModel.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                model.UpdatedBy = 1;
                model.UpdatedDate = DateTime.Now;
                _dbContext.ProductModel.Update(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.ProductModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.ProductModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion
    }
}
