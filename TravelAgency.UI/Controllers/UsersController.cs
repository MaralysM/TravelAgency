using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KCI_SecureModuleCL.Models;
using TravelAgency.Manager;
using TravelAgency.UI.ViewModels;
using TravelAgency.UI.Helper;
using Microsoft.AspNetCore.Http;

namespace TravelAgency.UI.Controllers
{
    public class UsersController : Controller
    {
        public IUserManager Manager { get; }


        public UsersController(IUserManager manager)
        {
            Manager = manager;

        }

        public async Task<IActionResult> Index()
        {
            return View(await Manager.All());
        }
        public async Task<IActionResult> Post(UserViewModel viewModel)
        {
            try
            {             
                if (!ModelState.IsValid)
                {
                    return View("Form", viewModel);
                }
                bool passWordChanged = (viewModel.TX_Password != null);

                SM_USER user = await Manager.Save(new SM_USER
                {
                    ID_User = viewModel.ID_User,
                    TX_Email = viewModel.TX_Email,
                    TX_FirstName = viewModel.TX_FirstName,
                    TX_SecondName = viewModel.TX_SecondName,
                    TX_LastName = viewModel.TX_LastName,
                    TX_SecondLastName = viewModel.TX_SecondLastName,
                    TX_Phone = viewModel.TX_Phone,
                    TX_Password = viewModel.TX_Password,
                    PasswordChanged = passWordChanged,
                });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View("Form", viewModel);
            }
        }
        public async Task<IActionResult> Get()
        {

            try
            {
                ViewData["Customer"] =  "";
                UserViewModel viewModel = new UserViewModel();
                return View("Form", viewModel);
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
                var data = await Manager.FindById(id);

                UserViewModel viewModel = new UserViewModel
                {
                    ID_User = data.ID_User,
                    TX_Email = data.TX_Email,
                    TX_FirstName = data.TX_FirstName,
                    TX_SecondName = data.TX_SecondName,
                    TX_LastName = data.TX_LastName,
                    TX_SecondLastName = data.TX_SecondLastName,
                    TX_Phone = data.TX_Phone,
                };

                return View("Form", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
