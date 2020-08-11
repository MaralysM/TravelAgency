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
    public class AutomaticTransitionController : Controller
    {
        public ITransitionParametersManager Manager { get; }
        public IElementManager ElementManager { get; }


        public AutomaticTransitionController(ITransitionParametersManager manager, IElementManager elementManager)
        {
            Manager = manager;
            ElementManager = elementManager;
        }
        public IActionResult Index(int Order = 0)
        {
            TransitionParametersDetails DataDetails = new TransitionParametersDetails();
            TransitionParametersHeader DataHeader = Manager.FindById(0).Result;
            string[] Element = null; 
            if (DataHeader.Active) {
                if (Order == DataHeader.transitionParametersDetails.OrderByDescending(x => x.order_transition).FirstOrDefault().order_transition)
                {
                    Order = 1;
                }
                else {
                    Order++;
                }
                
                DataDetails = DataHeader.transitionParametersDetails.OrderBy(x => x.order_transition).Where(x => x.order_transition >= Order).FirstOrDefault();
                Element = ElementManager.FindById(DataDetails.id_element).Result.TX_Url.Split("/");


            } else {
                ModelState.AddModelError("Error", "Automatic transition graphs are not active");
                return View();
            }

            return RedirectToAction($"{Element[1]}", $"{Element[0]}", new { Time = Manager.ConversionToMilliseconds(DataDetails.time_transition), Order= DataDetails.order_transition });

        }
    }
}
