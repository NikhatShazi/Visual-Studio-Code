using eGift.WebAPI.Common;
using eGift.WebAPI.Models.DataModels;
using eGift.WebAPI.Models.DBContexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace eGift.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Variables
        private ApplicationDBContext _dbContext;
        #endregion

        #region Constructors
        public LoginController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion

        #region Default CRUD Actions

        // GET: api/<LoginController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET api/<LoginController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //GET api/<loginController>/5
        [HttpGet("GetByRefId")]
        public IActionResult GetByRefId(int id, string type)
        {
            var model = _dbContext.LoginModel.Where(x => x.RefId == id && x.RefType == type).FirstOrDefault();
            return Ok(model);
        }

        //GET api/<loginController>/5
        [HttpGet("GetLoginEmployee")]
        public IActionResult GetLoginEmployee(string userName, string password)
        {
            var model = _dbContext.LoginModel.Where(x => x.UserName == userName && x.Password == password && (x.RefType==(RoleType.Employee).ToString() || x.RefType==(RoleType.Admin).ToString() || x.RefType==(RoleType.SuperAdmin).ToString())).FirstOrDefault();
            return Ok(model);
        }

        //GET api/<loginController>/5
        [HttpGet("GetLoginCustomer")]
        public IActionResult GetLoginCustomer(string userName, string password)
        {
            var model = _dbContext.LoginModel.Where(x => x.UserName == userName && x.Password == password && x.RefType==(RoleType.Customer).ToString()).FirstOrDefault();
            return Ok(model);
        }

        //POST api/<loginController>/5
        [HttpPost("SaveLoginDateTime")]
        public IActionResult SaveLoginDateTime(LoginModel model)
        {
            var loginModel = _dbContext.LoginModel.Where(x => x.ID == model.ID).FirstOrDefault();
            loginModel.LastLoginDate = loginModel.LoginDate;
            loginModel.LoginDate = model.LoginDate;
            _dbContext.Update(loginModel);
            _dbContext.SaveChanges();

            return Ok(loginModel);
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post(LoginModel model)
        {
            if(model.ID == 0)
            {
                _dbContext.Add(model);
                _dbContext.SaveChanges();
            }
            else
            {
                var dataModel = _dbContext.LoginModel.Where(x =>x.RefId == model.RefId && x.RefType == model.RefType).FirstOrDefault();
                dataModel.UserName = model.UserName;
                dataModel.Password = model.Password;    
                _dbContext.Update(dataModel);
                _dbContext.SaveChanges();
            }
            return Ok(model);
        }

        // PUT api/<LoginController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LoginController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion
    }
}
