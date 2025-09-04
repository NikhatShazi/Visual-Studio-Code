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
    public class CustomerController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public CustomerController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Default CRUD Actions
        // GET: api/<CustomerController>
        [HttpGet]
        public IActionResult Get()
        {
            var customerList = _dbContext.CustomerModel.Where(x => !x.IsDeleted).ToList();
            return Ok(customerList);
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var customer = _dbContext.CustomerModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(customer);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public IActionResult Post(CustomerModel model)
        {
            try
            {
                if (model.ID == 0)
                {
                    model.IsDeleted = false;
                    model.CreatedBy = 1; // Need to change
                    model.CreatedDate = DateTime.Now;
                    _dbContext.CustomerModel.Add(model);
                    _dbContext.SaveChanges();
                }
                else
                {
                    model.UpdatedBy = 1;
                    model.UpdatedDate = DateTime.Now;
                    _dbContext.CustomerModel.Update(model);
                    _dbContext.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Ok(model);
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.CustomerModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.CustomerModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion
    }
}
