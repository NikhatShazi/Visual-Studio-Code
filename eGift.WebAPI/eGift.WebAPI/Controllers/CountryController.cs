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
    public class CountryController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public CountryController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        #endregion

        #region Default CRUD Actions

       
        // GET: api/<CountryController>
        [HttpGet]
        public IActionResult Get()
        {
            var countryList = _dbContext.CountryModel.Where(x => !x.IsDeleted).ToList();
            return Ok(countryList);
        }

        // GET api/<CountryController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var country = _dbContext.CountryModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(country);
        }

        //GET: api/CountryController
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var countryList = _dbContext.CountryModel.ToList();
            return Ok(countryList);
        }

        //GET api/<Country Controller>/5
        [HttpGet("GetFromDelete")]
        public IActionResult GetFromDelete(int id)
        {
            var state = _dbContext.CountryModel.Where(x => x.ID == id).FirstOrDefault();
            return Ok(state);
        }

        // POST api/<CountryController>
        [HttpPost]
        public IActionResult Post(CountryModel model)
        {
                if (model.ID == 0)
                {
                    model.IsDeleted = false;
                    model.CreatedBy = 1; // Need to change
                    model.CreatedDate = DateTime.Now;
                    _dbContext.CountryModel.Add(model);
                    _dbContext.SaveChanges();
                }
                else
                {
                    model.UpdatedBy = 1;
                    model.UpdatedDate = DateTime.Now;
                    _dbContext.CountryModel.Update(model);
                    _dbContext.SaveChanges();
                }
            return Ok(model);
        }

        // PUT api/<CountryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CountryController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.CountryModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.CountryModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion
    }
}
