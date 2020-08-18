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
        public ITransitionParametersManager TransitionParametersManager { get; }
        public ReferenceParametersController(IReferenceParametersManager manager, IElementManager elementManager, ITransitionParametersManager transitionParametersManager)
        {
            Manager = manager;
            ElementManager = elementManager;
            TransitionParametersManager = transitionParametersManager;
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
                ValidateViewModel(viewModel);
                if (!ModelState.IsValid)
                {
                    viewModel.idElement = 0;
                    return View("Index", viewModel);
                }

                var entity = new ReferenceParameters
                {                   
                    id_element= viewModel.idElement,
                    reference = viewModel.Reference.Replace(".", ""),  
                    id_child = viewModel.idChild,
                    refmax = viewModel.RefMax ==null ? "" : viewModel.RefMax.Replace(".", ""),
                    refmin = viewModel.RefMin == null ? "" : viewModel.RefMin.Replace(".", "")
                };
                int id_average = viewModel.idAverage = TransitionParametersManager.GetByName("MTD Average").Result.id_element;
                long result = Manager.Save(entity, id_average);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "An error occurred while saving. Verify that the data is correct");
                return RedirectToAction("Index");
            }
        }

        private void ValidateViewModel(ReferenceParametersViewModel viewModel)
        {
            var data = Manager.All().Result;
            if (data.Any(x => x.id_element == viewModel.idAverage && x.id_child == viewModel.idChild))
            {
                ModelState.AddModelError("Error", "There is a record for the selected graph");
            }
        }
        private void InitializeViewModel(ReferenceParametersViewModel viewModel)
        {
            viewModel.idAverage = TransitionParametersManager.GetByName("MTD Average").Result.id_element;
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
        public ActionResult UpdateRefmin(short Id, string Ref)
        {
            ReferenceParameters entity = new ReferenceParameters();
            string RefValue = Ref.Replace(".", "");
            entity.Id = Id;
            entity.refmin = RefValue;
            var Result = Manager.UpdateReferenceMin(entity);

            return Json(new
            {
                aaData = Result

            });
        }
        [HttpGet]
        public ActionResult UpdateRefmax(short Id, string Ref)
        {
            ReferenceParameters entity = new ReferenceParameters();
            string RefValue = Ref.Replace(".", "");
            entity.Id = Id;
            entity.refmax = RefValue;
            var Result = Manager.UpdateReferenceMax(entity);

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
