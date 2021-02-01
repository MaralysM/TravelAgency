using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Manager;
using TravelAgency.UI.ViewModels;
using TravelAgency.UI.Helper;
using TravelAgency.Entities;


namespace TravelAgency.UI.Controllers
{
    public class RequestsController : Controller
    {
        public IRequestsManager Manager { get; }
        public ITravellersManager TravellersManager { get; }
        public ITravelsManager TravelsManager { get; }


        public RequestsController(IRequestsManager manager, ITravellersManager travellersManager, ITravelsManager travelsManager)
        {
            Manager = manager;
            TravellersManager = travellersManager;
            TravelsManager = travelsManager;
        }

        public async Task<IActionResult> Index()
        {
            RequestsViewModel viewModel = new RequestsViewModel();
            var TravellersList = await TravellersManager.All();
            var TravelList = await TravelsManager.All();
            FillTravellersList(viewModel, TravellersList.ToList());
            FillTravelList(viewModel, TravelList.ToList());
            viewModel.Requests = await Manager.All();
            return View(viewModel);
        }

        private void FillTravellersList(RequestsViewModel viewModel, List<Travellers> TravellersList) =>
            viewModel.TravellersList = SelectListItemHelper.ToSelectList(TravellersList.ToList(), "ID_Travellers", "Name", "Code", false);

        private void FillTravelList(RequestsViewModel viewModel, List<Travels> TravelsList) =>
             viewModel.TravelsList = SelectListItemHelper.ToSelectList(TravelsList.ToList(), "ID_Travels", "Name", "Code", false);

        public async Task<IActionResult> Post(RequestsViewModel viewModel)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    return View("Form", viewModel);
                }

                await Manager.Save(new Requests
                {
                    ID_Requests = viewModel.ID_Requests,
                    travellers = new Travellers { ID_Travellers = viewModel.Travellers.ID_Travellers },
                    travels = new Travels { ID_Travels = viewModel.Travels.ID_Travels }
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View("Form", viewModel);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            try
            {
                await Manager.Delete((int)id); return Ok();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }
        public async Task<IActionResult> GetUpdate(int id)
        {
            try
            {
                var entity = await Manager.FindById(id);
                var TravellersList = await TravellersManager.All();
                var TravelList = await TravelsManager.All();
                RequestsViewModel viewModel = new RequestsViewModel
                {
                    Travellers = new Travellers { ID_Travellers = entity.travellers.ID_Travellers },
                    Travels = new Travels { ID_Travels = entity.travels.ID_Travels },
                    ID_Requests = entity.ID_Requests
                };
                FillTravellersList(viewModel, TravellersList.ToList());
                FillTravelList(viewModel, TravelList.ToList());
                viewModel.Requests = await Manager.All();
                return View("Index", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> GetDetailsById(int idRequests)
        {

            var data = await Manager.FindById(idRequests);

            return Json(new { data = data});
        }
    }
}