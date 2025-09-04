using eGift.Admin.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace eGift.Admin.MVC.Controllers
{
    public class HomeController : Controller
    {
        #region Variables
        private readonly ILogger<HomeController> _logger;
        #endregion

        #region Constructors
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #endregion

        #region Default CRUD Actions
        public IActionResult Index()
        {
            return View();
        }
        #endregion

        #region Others Actions
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        #endregion
    }
}
