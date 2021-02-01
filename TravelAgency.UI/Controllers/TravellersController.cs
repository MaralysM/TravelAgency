using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelAgency.Manager;
using TravelAgency.UI.ViewModels;
using TravelAgency.Entities;


namespace TravelAgency.UI.Controllers
{
    public class TravellersController : Controller    {
        public ITravellersManager Manager { get; }


        public TravellersController(ITravellersManager manager)
        {
            Manager = manager;

        }

        public async Task<IActionResult> Index()
        {
            TravellersViewModel travellersViewModel = new TravellersViewModel();
            travellersViewModel.Travellers = await Manager.All();
            return View(travellersViewModel);
        }
        public async Task<IActionResult> Post(TravellersViewModel viewModel)
        {
            try
            {             
                
                if (!ModelState.IsValid)
                {
                    return View("Form", viewModel);
                }

                await Manager.Save(new Travellers
                {
                    ID_Travellers = viewModel.ID_Travellers,
                    TX_FirstName = viewModel.TX_FirstName,
                    TX_SecondName = viewModel.TX_SecondName,
                    TX_LastName = viewModel.TX_LastName,
                    TX_SecondLastName = viewModel.TX_SecondLastName,
                    TX_Phone = viewModel.TX_Phone,
                    TX_IdentificationCard =  viewModel.TX_IdentificationCard,
                    TX_Address = viewModel.TX_Address
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
                TravellersViewModel travellersViewModel = new TravellersViewModel();
                return View("Form", travellersViewModel);
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

                TravellersViewModel viewModel = new TravellersViewModel
                {
                    ID_Travellers = entity.ID_Travellers,
                    TX_FirstName = entity.TX_FirstName,
                    TX_SecondName = entity.TX_SecondName,
                    TX_LastName = entity.TX_LastName,
                    TX_SecondLastName = entity.TX_SecondLastName,
                    TX_Phone = entity.TX_Phone,
                    TX_IdentificationCard = entity.TX_IdentificationCard,
                    TX_Address =  entity.TX_Address
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
