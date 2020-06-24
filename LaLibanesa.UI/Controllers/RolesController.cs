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
using Qmos.UI.Filters;

namespace Qmos.UI.Controllers
{
    [SessionTimeoutAttribute]

    public class RolesController : Controller
    {
        public IRoleManager Manager { get; }

        public RolesController(IRoleManager manager)
        {
            Manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Manager.All());
        }

        public async Task<IActionResult> Post(RoleViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Form", viewModel);
                }

                SM_ROLE Role = new SM_ROLE
                {
                    ID_Element = viewModel.ID_Element,
                    TX_Role = viewModel.TX_Role,
                    TX_Description = viewModel.TX_Description,
                    ID_Role = viewModel.ID_Role,
                    BO_VisibleCliente = viewModel.BO_VisibleCliente
                };
                await Manager.Save(Role);
                int[] ID_Elemets = JsonConvert.DeserializeObject<int[]>(viewModel.ID_Elements);
                bool resul = Manager.UpdateRolesElements(Role.ID_Role, ID_Elemets).Result;
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
                RoleViewModel ViewModel = new RoleViewModel
                {
                    ListAplications = SelectListItemHelper.ToSelectList(Manager.AllApplications().Result.ToList(),
                                                                        nameof(SM_ELEMENT.ID_Element), nameof(SM_ELEMENT.TX_Name))
                };
                ViewModel.RoleTreeView = TreeBuilder.GenerateTree(
                    Manager.GetTreeView(HttpContext.Session.Get<int>("ID_Role")).Result, new StringBuilder()).Tree;
                return View("Form", ViewModel);
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

                return View("Form", new RoleViewModel
                {
                    ID_Element = (int)data.ID_Element,
                    ID_Role = data.ID_Role,
                    TX_Role = data.TX_Role,
                    TX_Description = data.TX_Description,
                    BO_VisibleCliente = data.BO_VisibleCliente,
                    ListAplications = SelectListItemHelper.ToSelectList(Manager.AllApplications().Result.ToList(),
                                                                        nameof(SM_ELEMENT.ID_Element), nameof(SM_ELEMENT.TX_Name)),
                    RoleTreeView = TreeBuilder.GenerateTree(Manager.GetTreeView(id).Result, new StringBuilder()).Tree
                }) ;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
