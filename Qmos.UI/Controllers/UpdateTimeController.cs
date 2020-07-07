using Microsoft.AspNetCore.Mvc;
using Qmos.Manager;
using Qmos.UI.Filters;
using System;
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
    }
}
