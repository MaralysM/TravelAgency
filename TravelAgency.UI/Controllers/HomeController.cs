using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TravelAgency.Manager;

namespace TravelAgency.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public IUserManager Manager { get; }
        public HomeController(ILogger<HomeController> logger, IUserManager manager)
        {
            Manager = manager;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {            
            return View(await Manager.All());
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
