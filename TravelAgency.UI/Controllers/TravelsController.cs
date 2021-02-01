using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Manager;
using TravelAgency.UI.ViewModels;
using TravelAgency.Entities;
using TravelAgency.UI.Helper;

namespace TravelAgency.UI.Controllers
{
    public class TravelsController : Controller
    {
        public ITravelsManager Manager { get; }


        public TravelsController(ITravelsManager manager)
        {
            Manager = manager;

        }

        public async Task<IActionResult> Index()
        {
            TravelsViewModel travellersViewModel = new TravelsViewModel();
            travellersViewModel.Travels = await Manager.All();
            return View(travellersViewModel);
        }
        public async Task<IActionResult> Post(TravelsViewModel viewModel)
        {
            try
            {             
                
                if (!ModelState.IsValid)
                {
                    return View("Form", viewModel);
                }

                await Manager.Save(new Travels
                {
                    ID_Travels = viewModel.ID_Travels,
                    NU_TravelCode = viewModel.NU_TravelCode,
                    NU_NumberOfPlace = viewModel.NU_NumberOfPlace,
                    TX_Destination = viewModel.TX_Destination,
                    TX_Origin = viewModel.TX_Origin,
                    NU_Price = StringToDecimalHelper.ApplyFormat(viewModel.NU_Price)
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View("Form", viewModel);
            }
        }
        public IActionResult Get()
        {

            try
            {
                TravelsViewModel travelsViewModel = new TravelsViewModel();
                return View("Form", travelsViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
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

                TravelsViewModel viewModel = new TravelsViewModel
                {
                    ID_Travels = entity.ID_Travels,
                    NU_TravelCode = entity.NU_TravelCode,
                    NU_NumberOfPlace = entity.NU_NumberOfPlace,
                    TX_Destination = entity.TX_Destination,
                    TX_Origin = entity.TX_Origin,
                    NU_Price = entity.NU_Price
                };
                return View("Form", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public void ValidateViewModel(UserViewModel viewModel)
        {

        }

    }
}
