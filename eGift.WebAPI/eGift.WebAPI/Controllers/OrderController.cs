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
    public class OrderController : ControllerBase
    {
        // GET: api/<OrderController>
        #region Variables

        private ApplicationDBContext _dbContext;

        #endregion

        #region Constructors

        public OrderController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Order Default CRUD Actions

        // GET: api/<OrderController>
        [HttpGet]
        public IActionResult Get()
        {
            var orderList = _dbContext.OrderModel.Where(x => !x.IsDeleted).ToList();
            return Ok(orderList);
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _dbContext.OrderModel.Where(x => !x.IsDeleted && x.ID == id).FirstOrDefault();
            return Ok(order);
        }

        // POST api/<OrderController>
        [HttpPost]
        public IActionResult Post(OrderModel model)
        {
            if (model.ID == 0)
            {
                model.IsDeleted = false;
                model.CreatedBy = 1;//Need to change
                model.CreatedDate = DateTime.Now;
                _dbContext.OrderModel.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                model.UpdatedBy = 1;
                model.UpdatedDate = System.DateTime.Now;
                _dbContext.OrderModel.Update(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var dataModel = _dbContext.OrderModel.Where(x => !x.IsDeleted & x.ID == id).FirstOrDefault();
            dataModel.IsDeleted = true;
            dataModel.UpdatedBy = 1;
            dataModel.UpdatedDate = DateTime.Now;
            _dbContext.OrderModel.Update(dataModel);
            _dbContext.SaveChanges();
            return Ok(id);
        }
        #endregion

        #region Order Other Actions
        // GET: api/<OrderController>
        [HttpGet("MyOrder")]
        public IActionResult MyOrder(int loginUserId)
        {
            var orderList = _dbContext.OrderModel.Where(x => !x.IsDeleted && x.CustomerId == loginUserId).ToList();
            return Ok(orderList);
        }
        #endregion
    }
}
