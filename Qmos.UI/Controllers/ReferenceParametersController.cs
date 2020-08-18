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
        public IElementManager ElementManager { get; }
        public ReferenceParametersController(IReferenceParametersManager manager, IElementManager elementManager)
        {
            Manager = manager;
            ElementManager = elementManager;
        }
        public IActionResult Index()
        {
            ReferenceParametersViewModel referenceParametersViewModel = new ReferenceParametersViewModel();
            InitializeViewModel(referenceParametersViewModel);
            return View(referenceParametersViewModel);
        }
        public IActionResult Post(ReferenceParametersViewModel viewModel)
        {
            InitializeViewModel(viewModel);
            try
            {
                if (!ModelState.IsValid)
                {
                    return RedirectToAction("Index");
                }

                var entity = new ReferenceParameters
                {
                    id_element= viewModel.idElement,
                    reference = viewModel.Reference.Replace(".", ""),  
                    id_child = viewModel.idChild,
                    refmax = viewModel.RefMax,
                    refmin = viewModel.RefMin
                };

                long result = Manager.Save(entity);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "An error occurred while saving. Verify that the data is correct");
                return RedirectToAction("Index");
            }
        }


        private void InitializeViewModel(ReferenceParametersViewModel viewModel)
        {
            List<KCI_SecureModuleCL.Models.SM_ELEMENT> EntityElement = ElementManager.All().Result.ToList();
            List<ChildElement> EntityChildElement = Manager.AllChildElement().Result.ToList();
            viewModel.ElementList = SelectListItemHelper.ToSelectList(EntityElement.Where(x => x.ID_ElementParent == EntityElement.Where(y => y.TX_Name == "Dashboards").FirstOrDefault().ID_Element).ToList(), "ID_Element", "TX_Name", "Code");
            viewModel.ChildList = SelectListItemHelper.ToSelectList(EntityChildElement, "Id", "Name", "Code");
            viewModel.List =  Manager.All().Result;
        }
        [HttpGet]
        public ActionResult UpdateRef(short Id, string Ref)
        {
            ReferenceParameters entity = new ReferenceParameters();
            string RefValue = Ref.Replace(".", "");
            entity.Id = Id;
                entity.reference = RefValue;
            var Result = Manager.UpdateReference(entity);

            return Json(new
            {
                aaData = Result

            });
        }


        [HttpGet]
        public ActionResult FindByIdElement(int id_element)
        {
            return Json(new
            {
                aaData = Manager.FindByIdElement(id_element)

            }); 
        }
        public IActionResult Remove(short id)
        {
            try
            {
                Manager.Remove(id);
                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }


    }
}
