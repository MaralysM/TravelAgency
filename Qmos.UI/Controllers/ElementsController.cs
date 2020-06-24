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
using Microsoft.AspNetCore.Http;
using Qmos.UI.Filters;

namespace Qmos.UI.Controllers
{
    [SessionTimeoutAttribute]

    public class ElementsController : Controller
    {
        public IElementManager Manager { get; }

        public ElementsController(IElementManager manager)
        {
            Manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await Manager.All());
        }

        public async Task<IActionResult> Post(ElementViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Form", viewModel);
                }

                SM_ELEMENT Element = new SM_ELEMENT
                {
                    ID_Element = viewModel.ID_Element,
                    ID_ElementParent = viewModel.ID_ElementParent,
                    ID_Type = viewModel.ID_Type,
                    TX_Icon = viewModel.TX_Icon,
                    TX_Name = viewModel.TX_Name,
                    TX_Url = viewModel.TX_Url
                };
                await Manager.Save(Element);
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
                ElementViewModel ViewModel = new ElementViewModel
                {
                    //ListElements = SelectListItemHelper.ToSelectList(Manager.All().Result.ToList(),
                    //nameof(SM_ELEMENT.ID_Element), nameof(SM_ELEMENT.TX_Name)),
                    ListElements = ElementViewModel.Convert(Manager.All().Result.ToList()),
                    ListTypes = SelectListItemHelper.ToSelectList(Manager.AllTypes().Result.ToList(),
                                                                        nameof(SM_ELEMENT_TYPE.ID_Type), nameof(SM_ELEMENT_TYPE.TX_Type))
                };

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

                return View("Form", new ElementViewModel
                {
                    ID_Element = data.ID_Element,
                    ID_ElementParent = data.ID_ElementParent,
                    ID_Type = data.ID_Type,
                    TX_Icon = data.TX_Icon,
                    TX_Name = data.TX_Name,
                    TX_Url = data.TX_Url,
                    ListElements = ElementViewModel.Convert(Manager.All().Result.ToList()),
                    //ListElements = SelectListItemHelper.ToSelectList(ElementViewModel.Convert(Manager.All().Result.ToList()),
                    //nameof(SM_ELEMENT.ID_Element), nameof(SM_ELEMENT.TX_Name)),
                    ListTypes = SelectListItemHelper.ToSelectList(Manager.AllTypes().Result.ToList(),
                                                                        nameof(SM_ELEMENT_TYPE.ID_Type), nameof(SM_ELEMENT_TYPE.TX_Type))
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public string[] GetElementsNotAuthorized()
        {

            if (HttpContext.Session.Get<string[]>("AuthorizedElements") == null)
            {
                HttpContext.Session.Set("AuthorizedElements", 
                    Manager.GetElementsNotAuthorized(int.Parse(HttpContext.Session.GetString("ID_Role"))));
            }
            return HttpContext.Session.Get<string[]>("AuthorizedElements");
        }
        [HttpPost]
        public IActionResult RegisterElements([FromBody]IList<SM_ELEMENT> Elements)
        {
            try
            {
                if(Elements.Count > 0)
                    Manager.RegisterElements(Elements);

                return Json(true);
            }
            catch (Exception)
            {
                return Json(false);
            }
        }
    }
}
