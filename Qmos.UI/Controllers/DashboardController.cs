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
        public IReferenceParametersManager ReferenceParametersManager { get; }

        public DashboardController(IDashboardManager manager, IReferenceParametersManager referenceParametersManager)
        {
            Manager = manager;
            ReferenceParametersManager = referenceParametersManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MTDMissedHeats(decimal Time = 0, short Order = 0)
        {
  
            return View("MTDMissedHeats", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order }) ;
        }

        public IActionResult MTDDelays(int id, decimal Time = 0, short Order = 0)
        {
            return View("MTDDelays", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }

        public IActionResult MTDTapTempandO2PPM(int id, decimal Time = 0, short Order = 0)
        {
            return View("MTDTapTempandO2PPM", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }
        public IActionResult KWhPerScrapTon(int id, decimal Time = 0, short Order = 0)
        {
            return View("KWhPerScrapTon", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }

        public IActionResult TonPerHour(int id, decimal Time = 0, short Order = 0)
        {
            return View("TonPerHour", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }

        public IActionResult IronYield(int id, decimal Time = 0, short Order = 0)
        {
            return View("IronYield", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }
        public IActionResult TargetPPM(int id, decimal Time = 0, short Order = 0)
        {          
            return View("TargetPPM", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = id });
        }
        public IActionResult TargetTemp(int id, decimal Time = 0, short Order = 0)
        {
            return View("TargetTemp", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }
        public IActionResult TapWtTarget(int id, decimal Time = 0, short Order = 0)
        {
            return View("TapWtTarget", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }
        public IActionResult MTDProduction(int id, decimal Time = 0, short Order = 0)
        {
            return View("MTDProduction", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }
        public IActionResult MTDAverage(decimal Time = 0, short Order = 0)
        {
            return View("MTDAverage", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order });
        }

    }
}
