using eGift.WebAPI.Models.DataModels;
using eGift.WebAPI.Models.DBContexts;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailsController : ControllerBase
    {
        #region Variables

        private ApplicationDBContext _dbContext;

        #endregion

        #region Constructors

        public OrderDetailsController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion

        #region Order Details Default CRUD Actions
        // GET: api/<OrderDetialsController>
        [HttpGet]
        public string[] Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrderDetialsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrderDetialsController>
        [HttpPost]
        public IActionResult Post(OrderDetailsModel model)
        {
            if (model.ID == 0)
            {
                _dbContext.OrderDetailsModel.Add(model);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<OrderDetialsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrderDetialsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion

        #region Order Details Other Actions
        // GET api/<OrderDetialsController>/5
        [HttpGet("GetByOrderId")]
        public IActionResult GetByOrderId(int orderId)
        {
            var orderDetailList = _dbContext.OrderDetailsModel.Where(x => !x.IsDeleted && x.OrderId == orderId).ToList();
            return Ok(orderDetailList);
        }
        #endregion
    }
}
