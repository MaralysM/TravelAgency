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
using System.Globalization;

namespace Qmos.UI.Controllers
{
    [SessionTimeoutAttribute]
    public class ReferenceParametersController : Controller
    {
        public IReferenceParametersManager Manager { get; }


        public ReferenceParametersController(IReferenceParametersManager manager)
        {
            Manager = manager;
        }
        public async Task<IActionResult> Index()
        {
            IList<ReferenceParameters> EntityList = new List<ReferenceParameters>();
            return View(await Manager.All());
        }
        [HttpGet]
        public ActionResult UpdateRef(short Id, short TypeRef, string Ref)
        {
            ReferenceParameters entity = new ReferenceParameters();
            string RefValue = Ref.Replace(".", "");
            entity.Id = Id;
            if (TypeRef == 1) {
                entity.ref1 = RefValue;
                entity.ref2 = "0";//Unicamente como indicador
            } else {
                entity.ref1 = "0";////Unicamente como indicador
                entity.ref2 = RefValue;
            }

            var Result = Manager.UpdateReference(entity);

            return Json(new
            {
                aaData = Result

            });
        }

    }
}
