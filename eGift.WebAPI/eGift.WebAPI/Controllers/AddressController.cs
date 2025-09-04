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
    public class AddressController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion


        #region Constructors
        public AddressController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Default CRUD Actions


        // GET: api/<AddressController>
        [HttpGet]
        public IActionResult Get()
        {
            var addressList = _dbContext.AddressModel.Where(x => !x.IsDeleted).ToList();
            return Ok(addressList);
        }

        // GET api/<AddressController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var address = _dbContext.AddressModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(address);
        }

        //GET: api/CityController
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var addressList = _dbContext.AddressModel.ToList();
            return Ok(addressList);
        }


        //GET api/<Address Controller>/5
        [HttpGet("GetFromDelete")]
        public IActionResult GetFromDelete(int id)
        {
            var address = _dbContext.AddressModel.Where(x => x.ID == id).FirstOrDefault();
            return Ok(address);
        }

        // POST api/<AddressController>
        [HttpPost]
        public IActionResult Post(AddressModel model)
        {
            if (model.ID == 0)
            {
                model.IsDeleted = false;
                model.CreatedBy = 1; // Need to change
                model.CreatedDate = DateTime.Now;
                _dbContext.AddressModel.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                model.UpdatedBy = 1;
                model.UpdatedDate = DateTime.Now;
                _dbContext.AddressModel.Update(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<AddressController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AddressController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.AddressModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.AddressModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion
    }
}
