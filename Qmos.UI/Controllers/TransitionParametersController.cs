using Qmos.Entities;
using Qmos.Manager;
using Qmos.UI.Helper;
using Qmos.UI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace KeyCore.TimeSheet.UI.Controllers
{
    public class TransitionParametersController : Controller
    {
        public ITransitionParametersManager Manager { get; }
        public IElementManager ElementManager { get; }
        public TransitionParametersController(ITransitionParametersManager manager, IElementManager elementManager)
        {
            Manager = manager;
            ElementManager = elementManager;
        }

        public async Task<IActionResult> Index()
        {
            IList<TransitionParametersHeader> list = new List<TransitionParametersHeader>();
            try
            {
                list = await Manager.All();
                return View(list);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View(list);
            }
        }

        //public IActionResult GetUpdateDetail(int id)
        //{
        //    try
        //    {
        //        var result = Manager.FindDetailById(id);
        //        return Json(result);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { error = true, message = ex.Message });
        //    }
        //}

        public IActionResult Post(TransitionParametersViewModel viewModel)
        {
            InitializeViewModel(viewModel);
            try
            {
                //ValidateViewModel(viewModel);

                if (!ModelState.IsValid)
                {
                    //var data = Manager.FindById(viewModel.Header.Id);
                    //viewModel.List = data.ScheduleEmployeeList;
                    //return View("Form", viewModel);
                }

                var entity = new TransitionParametersHeader
                {
                    Id = viewModel.Header.Id,
                    Name = "",
                    Active = true
                };

                var entityDetails = new TransitionParametersDetails
                { 
                    time_transition = viewModel.Detail.TimeTransition,
                    order_transition =  viewModel.Detail.OrderTransition,
                    id_element = viewModel.Detail.IdElement
                };
                //if (viewModel.Detail.ListOfSelectedWorkShift == null ? viewModel.Detail.Id_WorkShift != 0 : viewModel.Detail.ListOfSelectedWorkShift.Count() != 0 && viewModel.Detail.idJob != 0 && viewModel.Detail.OrderJob != 0)
                //{
                //    if (viewModel.Detail.ListOfSelectedWorkShift != null)
                //    {
                //        foreach (var item in viewModel.Detail.ListOfSelectedWorkShift)
                //        {
                //            entity.ScheduleEmployeeList.Add(new TemplateJobDetails
                //            {
                //                Id = viewModel.Detail.Id,
                //                Job = new Job { Id = viewModel.Detail.idJob },
                //                ParametersPlantHeaderShift = new ParametersPlantHeaderShift { Id = item },
                //                Orderjob = viewModel.Detail.OrderJob
                //            });
                //        }
                //    }
                //    else
                //    {
                //        entity.ScheduleEmployeeList.Add(new TemplateJobDetails
                //        {
                //            Id = viewModel.Detail.Id,
                //            Job = new Job { Id = viewModel.Detail.idJob },
                //            ParametersPlantHeaderShift = new ParametersPlantHeaderShift { Id = viewModel.Detail.Id_WorkShift },
                //            Orderjob = viewModel.Detail.OrderJob
                //        });
                //    }
                //}

                long result = Manager.Save(entity);
                return RedirectToAction("GetUpdate", new { pk = result });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View("Form", viewModel);
            }
        }


        //private void ValidateViewModel(TemplateScheduleJobViewModel viewModel)
        //{
        //    if (viewModel.Detail.ListOfSelectedWorkShift == null ? viewModel.Detail.Id_WorkShift == 0 : viewModel.Detail.ListOfSelectedWorkShift.Count() == 0)
        //        ModelState.AddModelError("Shift", "The Shift field is required");
        //    if (viewModel.Detail.idJob == 0)
        //        ModelState.AddModelError("Job", "The Job field is required");
        //    if (viewModel.Detail.OrderJob < 0)
        //        ModelState.AddModelError("Order", "The Order field is required ( > 0)");

        //    var ss = Manager.AllDetailByIdHeader(viewModel.Header.Id);
        //    if (ss.Any(x => x.Orderjob == viewModel.Detail.OrderJob && (viewModel.Detail.ListOfSelectedWorkShift == null ? x.ParametersPlantHeaderShift.Id == viewModel.Detail.Id_WorkShift : viewModel.Detail.ListOfSelectedWorkShift.Contains(x.ParametersPlantHeaderShift.Id)) && x.Id != viewModel.Detail.Id))
        //    {
        //        ModelState.AddModelError("Order", "The Order already has a template");
        //    }
        //    if (ss.Any(x => x.Job.Id == viewModel.Detail.idJob && (viewModel.Detail.ListOfSelectedWorkShift == null ? x.ParametersPlantHeaderShift.Id == viewModel.Detail.Id_WorkShift : viewModel.Detail.ListOfSelectedWorkShift.Contains(x.ParametersPlantHeaderShift.Id)) && x.Orderjob == viewModel.Detail.OrderJob && x.Id == viewModel.Header.Id))
        //    {
        //        ModelState.AddModelError("Job", "The Job already has a template");
        //    }

        //    if (ss.Any(x => x.Job.Id == viewModel.Detail.idJob && (viewModel.Detail.ListOfSelectedWorkShift == null ? x.ParametersPlantHeaderShift.Id == viewModel.Detail.Id_WorkShift : viewModel.Detail.ListOfSelectedWorkShift.Contains(x.ParametersPlantHeaderShift.Id)) && viewModel.Detail.Id == 0))
        //    {
        //        ModelState.AddModelError("Job", "The Job already has a template");
        //    }

        //}

        private void InitializeViewModel(TransitionParametersViewModel viewModel)
        {
            List<KCI_SecureModuleCL.Models.SM_ELEMENT> EntityElement = ElementManager.All().Result.ToList();             
            viewModel.Detail.ElementList = SelectListItemHelper.ToSelectList(EntityElement.Where(x=>x.ID_ElementParent == EntityElement.Where(y=>y.TX_Name== "Dashboards").FirstOrDefault().ID_Element).ToList(), "ID_Element", "TX_Name", "Code");
        }

        public IActionResult Get()
        {
            try
            {
                TransitionParametersViewModel transitionParametersViewModel = new TransitionParametersViewModel();
                InitializeViewModel(transitionParametersViewModel);
                return View("Form", transitionParametersViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public IActionResult GetUpdate(long pk)
        //{
        //    TemplateScheduleJobViewModel viewModel = new TemplateScheduleJobViewModel();
        //    InitializeViewModel(viewModel);
        //    try
        //    {
        //        var data = Manager.FindById(pk);
        //        viewModel.Header.Id = data.Id;

        //        viewModel.Header.Employee.Name = string.Concat(data.Employee.personSupervisor.FirstName.ToString(), " ", data.Employee.personSupervisor.SecondName.ToString(), " ", data.Employee.personSupervisor.LastName.ToString(), " ", data.Employee.personSupervisor.SecondLastName.ToString()).ToUpper();
        //        viewModel.List = data.ScheduleEmployeeList;
        //        return View("Form", viewModel);
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("Error", ex.Message);
        //        return View("Form", viewModel);
        //    }
        //}


        //public IActionResult Remove(long id)
        //{
        //    try
        //    {
        //        Manager.Remove(id);
        //        return Json(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = ex.Message });
        //    }
        //}

        //public IActionResult RemoveDetail(long id)
        //{
        //    try
        //    {
        //        Manager.RemoveDetail(id);
        //        return Json(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(new { success = false, error = ex.Message });
        //    }
        //}

    }
}
