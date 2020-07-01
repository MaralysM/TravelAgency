using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KCI_SecureModuleCL.Models;
using Qmos.Manager;
using Qmos.UI.ViewModels;
using Qmos.UI.Helper;
using Qmos.UI.Models;
using System.Text;
using Newtonsoft.Json;
using Qmos.Entities;
using Qmos.Manager.Abstractions;
using Microsoft.AspNetCore.Http;
using Qmos.UI.Filters;

namespace Qmos.UI.Controllers
{
    [SessionTimeoutAttribute]
    public class DashboardController : Controller
    {
        public IUserManager Manager { get; }


        public DashboardController(IUserManager manager)
        {
            Manager = manager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MTDMissedHeats()
        {
            return View("MTDMissedHeats");
        }

        public IActionResult MTDDelays()
        {
            return View("MTDDelays");
        }

        public IActionResult MTDTapTempand02PPM()
        {
            return View("MTDTapTempand02PPM");
        }
        public IActionResult KWhPerScrapTon()
        {
            return View("KWhPerScrapTon");
        }

        public IActionResult ScrapTonPerHour()
        {
            return View("ScrapTonPerHour");
        }

        public IActionResult IronYield()
        {
            return View("IronYield");
        }
        public IActionResult TargetPPM()
        {
            return View("TargetPPM");
        }
        public IActionResult TargetTemp()
        {
            return View("TargetTemp");
        }
        public IActionResult TapWtTarget()
        {
            return View("TapWtTarget");
        }       
    }
}
