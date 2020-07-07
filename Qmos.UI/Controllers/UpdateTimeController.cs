using Microsoft.AspNetCore.Mvc;
using Qmos.Manager;
using Qmos.UI.Filters;

namespace Qmos.UI.Controllers
{
    [SessionTimeoutAttribute]
    public class UpdateTimeController : Controller
    {
        public IUserManager Manager { get; }


        public UpdateTimeController(IUserManager manager)
        {
            Manager = manager;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
