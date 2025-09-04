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
    public class CategoryController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Default CRUD Actions


        // GET: api/<CategoryController>
        [HttpGet]
        public IActionResult Get()
        {
            var categoryList = _dbContext.CategoryModel.Where(x => !x.IsDeleted).ToList();
            return Ok(categoryList);
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var category = _dbContext.CategoryModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(category);
        }

        //GET: api/CategoryController
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categoryList = _dbContext.CategoryModel.ToList();
            return Ok(categoryList);
        }

        //GET api/<Category Controller>/5
        [HttpGet("GetFromDelete")]
        public IActionResult GetFromDelete(int id)
        {
            var subCategory = _dbContext.CategoryModel.Where(x => x.ID == id).FirstOrDefault();
            return Ok(subCategory);
        }

        // POST api/<CategoryController>
        [HttpPost]
        public IActionResult Post(CategoryModel model)
        {
            if (model.ID == 0)
            {
                model.IsDeleted = false;
                model.CreatedBy = 1; // Need to change
                model.CreatedDate = DateTime.Now;
                _dbContext.CategoryModel.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                model.UpdatedBy = 1;
                model.UpdatedDate = DateTime.Now;
                _dbContext.CategoryModel.Update(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }


        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.CategoryModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.CategoryModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion
    }
}
