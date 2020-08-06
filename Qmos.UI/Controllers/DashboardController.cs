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
        public IDashboardManager Manager { get; }


        public DashboardController(IDashboardManager manager)
        {
            Manager = manager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MTDMissedHeats()
        {
  
            return View("MTDMissedHeats", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result }) ;
        }

        public IActionResult MTDDelays()
        {
            return View("MTDDelays", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }

        public IActionResult MTDTapTempandO2PPM()
        {
            return View("MTDTapTempandO2PPM", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }
        public IActionResult KWhPerScrapTon()
        {
            return View("KWhPerScrapTon", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }

        public IActionResult ScrapTonPerHour()
        {
            return View("ScrapTonPerHour", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }

        public IActionResult IronYield()
        {
            return View("IronYield", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }
        public IActionResult TargetPPM()
        {
            return View("TargetPPM", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }
        public IActionResult TargetTemp()
        {
            return View("TargetTemp", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }
        public IActionResult TapWtTarget()
        {
            return View("TapWtTarget", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }
        public IActionResult MTDProduction()
        {
            return View("MTDProduction", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }
        public IActionResult MTDAverage()
        {
            return View("MTDAverage", new UpdateTimeViewModel { TIMEMILLISECONDS = Manager.ConversionToMilliseconds().Result });
        }

    }
}
