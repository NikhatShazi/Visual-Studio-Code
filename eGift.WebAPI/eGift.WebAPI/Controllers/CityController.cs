using eGift.WebAPI.Models.DataModels;
using eGift.WebAPI.Models.DBContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructions
        public CityController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        #endregion

        #region Default CRUD Actions

        // GET: api/<CityController>
        [HttpGet]
        public IActionResult Get()
        {
            var cityList = _dbContext.CityModel.Where(x => !x.IsDeleted).ToList();
            return Ok(cityList);
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var city = _dbContext.CityModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(city);
        }

        //GET: api/StateController
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var cityList = _dbContext.CityModel.ToList();
            return Ok(cityList);
        }

        //GET api/<City Controller>/5
        [HttpGet("GetFromDelete")]
        public IActionResult GetFromDelete(int id)
        {
            var city = _dbContext.CityModel.Where(x => x.ID == id).FirstOrDefault();
            return Ok(city);
        }
        // POST api/<CityController>
        [HttpPost]
        public IActionResult Post(CityModel model)
        {
            if (model.ID == 0)
            {
                model.IsDeleted = false;
                model.CreatedBy = 1; // Need to change
                model.CreatedDate = DateTime.Now;
                _dbContext.CityModel.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                model.UpdatedBy = 1;
                model.UpdatedDate = DateTime.Now;
                _dbContext.CityModel.Update(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.CityModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.CityModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion

        #region Ajax Actions
        //GET: api/CityController
        [HttpGet("GetByStateId")]
        public IActionResult GetByStateId(int stateId)
        {
            var cityList = _dbContext.CityModel.Where(x => !x.IsDeleted && x.StateId == stateId).ToList();
            return Ok(cityList);
        }
        #endregion

    }
}
