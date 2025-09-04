using eGift.WebAPI.Models.DataModels;
using eGift.WebAPI.Models.DBContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public SubCategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Default CRUD Actions


        // GET: api/<SubCategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var subCategoryList = _dbContext.SubCategoryModel.Where(x => !x.IsDeleted).ToList();
            return Ok(subCategoryList);
        }

        // GET api/<SubCategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var subCategory = _dbContext.SubCategoryModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(subCategory);
        }


        //GET: api/CategoryController
        [HttpGet("GetAll")]
        public IActionResult GetAll()

        {
            var subCategoryList = _dbContext.SubCategoryModel.ToList();
            return Ok(subCategoryList);
        }

        //GET api/<SubCategory Controller>/5
        [HttpGet("GetFromDelete")]
        public IActionResult GetFromDelete(int id)
        {
            var product = _dbContext.SubCategoryModel.Where(x => x.ID == id).FirstOrDefault();
            return Ok(product);
        }

        // POST api/<SubCategoryController>
        [HttpPost]
        public IActionResult Post(SubCategoryModel model)
        {
            if (model.ID == 0)
            {
                model.IsDeleted = false;
                model.CreatedBy = 1; // Need to change
                model.CreatedDate = DateTime.Now;
                _dbContext.SubCategoryModel.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                model.UpdatedBy = 1;
                model.UpdatedDate = DateTime.Now;
                _dbContext.SubCategoryModel.Update(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<SubCategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SubCategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.SubCategoryModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.SubCategoryModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion

        #region Ajax Actions
        //GET: api/<SubCategoryController>/GetByCategoryId?categoryId=5
        [HttpGet("GetByCategoryId")]
        public IActionResult GetByCategoryId(int categoryId)
        {
            var subCategoryList = _dbContext.SubCategoryModel.Where(x => !x.IsDeleted && x.CategoryId == categoryId).ToList();
            return Ok(subCategoryList);
        }
        #endregion
    }
}
