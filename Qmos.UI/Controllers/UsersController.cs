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
              
                viewModel.ListAplications = SelectListItemHelper.ToSelectList(Manager.AllApplications().Result.ToList(),
                nameof(SM_ELEMENT.ID_Element), nameof(SM_ELEMENT.TX_Name));
                
                ValidateViewModel(viewModel);
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
                    BO_Active = viewModel.BO_Active,
                    TX_Password = viewModel.TX_Password,
                    BO_PasswordExpired = false,
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
                var (rol, user) = await SearchUser();
                    ViewData["Customer"] =  "";
                
                UserViewModel viewModel = new UserViewModel
                {
                    TX_Link =  null,
                    BO_PasswordExpired = false,
                    ShowClient = true
                    
                };
                //if (viewModel.ListEmployees != null)
                //    viewModel.ListEmployees = viewModel.ListEmployees.Append(new SelectListItem { Selected = true, Text = "", Value = "" });
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
                var (rol, user) = await SearchUser();
                var data = await Manager.FindById(id);


                UserViewModel viewModel = new UserViewModel
                {
                    ID_User = data.ID_User,
                    BO_Active = data.BO_Active,
                    BO_PasswordExpired = data.BO_PasswordExpired,
                    TX_Email = data.TX_Email,
                    TX_FirstName = data.TX_FirstName,
                    TX_SecondName = data.TX_SecondName,
                    TX_LastName = data.TX_LastName,
                    TX_SecondLastName = data.TX_SecondLastName,
                    TX_Phone = data.TX_Phone,
                    BO_UpdateStatus = false,
                    ListAplications = SelectListItemHelper.ToSelectList(Manager.AllApplications().Result.ToList(),
                    nameof(SM_ELEMENT.ID_Element), nameof(SM_ELEMENT.TX_Name)),
                    RolesUser = Manager.GetRolesByUser(data.ID_User)
                };


                viewModel.ShowClient = viewModel.RolesUser.Any(r => r.ID_RoleNavigation.BO_VisibleCliente);

                return View("Form", viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public async Task<IActionResult> AddRole(UserRoleViewModel viewModel)
        {
            var UserRole = new SM_ROLE_USER
            {
                ID_Role = viewModel.ID_Role,
                ID_User = viewModel.ID_UserR
            };
            Manager.AddRoleToUser(UserRole);
            return await GetUpdate(viewModel.ID_UserR);
        }
        public IActionResult DeleteRoleByUser(int? id)
        {
            Manager.DeleteRoleByUser((int)id);
            return Ok();
        }
        public IActionResult GetRolesByApplication(int id)
        {
            return Json(new { result = Manager.GetRolesByApplication(id) });
        }
        public IActionResult GetRolesByApplicationAndValisibleByCliente(int id)
        {
            return Json(new { result = Manager.GetRolesByApplicationAndValisibleByCliente(id) });
        }
        public bool CheckUserEmail(string useremail)
        {
            return Manager.CheckUserEmail(useremail);
        }

        public async Task<(string rol, SM_USER user)> SearchUser()
        {
            string rol = HttpContext.Session.GetString("UserRole");
            SM_USER user = await Manager.FindById(int.Parse(HttpContext.Session.GetString("ID_User")));

            return (rol, user);
        }
        public void ValidateViewModel(UserViewModel viewModel)
        {
            if (CheckUserEmail(viewModel.TX_Email) && viewModel.ID_User == 0)
                ModelState.AddModelError("Email", "El correo ya existe en el sistema");
        }



    }
}
