using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using TravelAgency.UI.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace TravelAgency.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _UserService;

        public AccountController(IUserService UserService)
        {
            _UserService = UserService;   
        }
        [HttpPost]
        public IActionResult Login(AuthenticateModel Autentication)
        {
            try
            {
                SM_USER User = _UserService.Authenticate(Autentication.Useremail, Autentication.Password, 0, Autentication.Link);

                if (User != null)
                {
                        HttpContext.Session.Set("User", User);
                        HttpContext.Session.SetString("UserFullName", User.TX_FirstName + " " + User.TX_LastName);
                        return Redirect("/Home");
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
                ViewBag.ErrorMessage = "Ocurrió un error: " + e.Message;
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
    }
}