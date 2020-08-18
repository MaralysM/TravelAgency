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
        public ITransitionParametersManager TransitionParametersManager { get; }

        public DashboardController(IDashboardManager manager, ITransitionParametersManager transitionParametersManager)
        {
            Manager = manager;
            TransitionParametersManager = transitionParametersManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult MTDMissedHeats(decimal Time = 0, short Order = 0)
        {
  
            return View("MTDMissedHeats", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order }) ;
        }

        public async Task<IActionResult> MTDDelays( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("MTD Delays");
            return View("MTDDelays", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }

        public async Task<IActionResult> MTDTapTempandO2PPM( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("MTD Tap Temp and O2 PPM");
            return View("MTDTapTempandO2PPM", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }
        public async Task<IActionResult> KWhPerScrapTon( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("KWh per Ton");
            return View("KWhPerScrapTon", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }

        public async Task<IActionResult> TonPerHour( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("Ton per Hour");
            return View("TonPerHour", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }

        public async Task<IActionResult> IronYield( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("Iron Yield");
            return View("IronYield", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }
        public async Task<IActionResult> TargetPPM( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("Target PPM");
            return View("TargetPPM", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element});
        }
        public async Task<IActionResult> TargetTemp( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("Target Temp");
            return View("TargetTemp", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }
        public async Task<IActionResult> TapWtTarget( decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("TapWtTarget");
            return View("TapWtTarget", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }
        public async Task<IActionResult> MTDProduction(decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("MTD Production");
            return View("MTDProduction", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }
        public async Task<IActionResult> MTDAverage(decimal Time = 0, short Order = 0)
        {
            var Element = await TransitionParametersManager.GetByName("MTD Average");
            return View("MTDAverage", new UpdateTimeViewModel { TIMEMILLISECONDS = Time == 0 ? Manager.ConversionToMilliseconds().Result : Time, ORDER_TRANSITION = Order, IdElement = Element.id_element });
        }

    }
}
