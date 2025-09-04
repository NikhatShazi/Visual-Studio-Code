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
    public class StateController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public StateController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        #endregion

        #region Default CRUD Actions


        // GET: api/<StateController>
        [HttpGet]
        public IActionResult Get()
        {
            var stateList = _dbContext.StateModel.Where(x => !x.IsDeleted).ToList();
            return Ok(stateList);
        }

        // GET api/<StateController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var state = _dbContext.StateModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(state);
        }


        //GET: api/CountryController
        [HttpGet("GetAll")]
        public IActionResult GetAll()

        {
            var stateList = _dbContext.StateModel.ToList();
            return Ok(stateList);
        }

        //GET api/<State Controller>/5
        [HttpGet("GetFromDelete")]
        public IActionResult GetFromDelete(int id)
        {
            var state = _dbContext.StateModel.Where(x => x.ID == id).FirstOrDefault();
            return Ok(state);
        }

        // POST api/<StateController>
        [HttpPost]
        public IActionResult Post(StateModel model)
        {
            if (model.ID == 0)
            {
                model.IsDeleted = false;
                model.CreatedBy = 1; // Need to change
                model.CreatedDate = DateTime.Now;
                _dbContext.StateModel.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                model.UpdatedBy = 1;
                model.UpdatedDate = DateTime.Now;
                _dbContext.StateModel.Update(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<StateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<StateController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.StateModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.StateModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }

        #endregion

        #region Ajax Actions
        //GET: api/<StateController>/GetByCountryId?countryId=5
        [HttpGet("GetByCountryId")]
        public IActionResult GetByCountryId(int countryId)
        {
            var stateList = _dbContext.StateModel.Where(x => !x.IsDeleted && x.CountryId == countryId).ToList();
            return Ok(stateList);
        }
        #endregion

    }
}

