using Microsoft.AspNetCore.Mvc;
using Qmos.Entities;
using Qmos.Manager;
using Qmos.UI.Filters;
using Qmos.UI.ViewModels;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace Qmos.UI.Controllers
{
    [SessionTimeoutAttribute]
    public class UpdateTimeController : Controller
    {
        public IUpdateTimeManager Manager { get; }


        public UpdateTimeController(IUpdateTimeManager manager)
        {
            Manager = manager;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                return View(await Manager.All(false));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View();
            }
        }
        public IActionResult Get() => View("Form", new UpdateTimeViewModel() { ACTIVE = true });

        public async Task<IActionResult> Post(UpdateTimeViewModel viewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Form", viewModel);
                }

                await Manager.Save(new UpdateTime { Id = viewModel.Id, time_refresh = viewModel.TIME_REFRESH });
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View("Form", viewModel);
            }
        }

        public async Task<IActionResult> GetUpdate(short pk)
        {
            UpdateTimeViewModel viewModel = new UpdateTimeViewModel();
            try
            {
                var data = await Manager.FindById(pk);
                viewModel = new UpdateTimeViewModel { Id = data.Id, TIME_REFRESH = data.time_refresh };
                return View("Form", viewModel);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", ex.Message);
                return View("Form", viewModel);
            }
        }

        public async Task<IActionResult> Delete(short id)
        {
            try
            {
                await Manager.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message });
            }
        }

    }
}
