using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using Qmos.Manager;
using Qmos.Manager.Abstractions;
using Qmos.UI.Helper;
using Qmos.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Qmos.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IUserService _UserService;
        private readonly IElementService _ElementService;
        private readonly IAccountManager _AccountManager;
        private readonly IServiceScopeFactory ScopeFactory;

        //private readonly RequestHandler _requestHandler;

        public AccountController(ILogger<AccountController> logger, IServiceScopeFactory scopeFactory, IUserService UserService, IElementService ElementService, IAccountManager AccountManager)
        {
            _logger = logger;
            _UserService = UserService;
            _ElementService = ElementService;
            _AccountManager = AccountManager;
            
        }


        // GET: Account
        [HttpPost]
        public IActionResult Login(AuthenticateModel Autentication)
        {
            try
            {
                SM_USER User = _UserService.Authenticate(Autentication.Useremail, Autentication.Password, 0, Autentication.Link);

                if (User != null)
                {
                    if (!User.BO_Active)
                    {
                        ViewBag.SendEmailPassword = "";
                        ViewBag.ErrorMessage = "El usuario se encuentra inactivo";
                        return View();
                    }
                    else
                    {


                        MenuResult Menu = MenuBuilder.GenerateMenu(_ElementService.GetMenuByRole(User.CurrentRole.ID_Role, true, false), new StringBuilder(), true);
                        string DefaultUrl = _ElementService.GetMenuUrlDefault(User.CurrentRole.ID_Role, true, false);

                        HttpContext.Session.Set("User", User);
                        HttpContext.Session.SetString("UserFullName", User.TX_FirstName + " " + User.TX_LastName);
                        HttpContext.Session.SetString("UserRole", User.CurrentRole.TX_Role);
                        HttpContext.Session.Set("ID_Role", User.CurrentRole.ID_Role);
                        HttpContext.Session.SetString("Menu", Menu.Menu);
                        HttpContext.Session.Set("ID_User", User.ID_User);
                        UserManager.CurrentUserId = User.ID_User;
                        HttpContext.Session.SetString("CardCode", User.TX_Link == null ? "" : User.TX_Link);
                        HttpContext.Session.Set<long>("Id_PriceList", User.ID_PriceList == null ?  1 : (long) User.ID_PriceList);
                        return Redirect(DefaultUrl);

                    }
                } 
                else
                {
                    ViewBag.SendEmailPassword = "";
                    ViewBag.ErrorMessage = "El correo o contraseña son incorrectos";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.SendEmailPassword = "";
                ViewBag.ErrorMessage = "Ha ocurrido un error: " + e.Message;
                return View();
            }


        }

        public IActionResult Login()
        {
            ViewBag.SendEmailPassword = "";
            return View();
        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
        public async Task<IActionResult> ForgotPasswordeEmail(AuthenticateModel Autentication)
        {
            try
            {
                bool emailLog = await _AccountManager.ForgotPasswordNotification(Autentication.Useremail);
                ViewBag.SendEmailPassword = emailLog ? "ok" : "error";
                return View("Login");
            }
            catch (Exception e)
            {
                ViewBag.SendEmailPassword = "";
                ViewBag.ErrorMessage = "Ha ocurrido un error: " + e.Message;
                return View("Login");
            }

        }

    }
}