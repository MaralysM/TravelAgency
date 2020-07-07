using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using Qmos.Manager;
using Qmos.Manager.Abstractions;
using Qmos.UI.Filters;
using Qmos.UI.Helper;
using Qmos.UI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Qmos.UI.Controllers
{
    [SessionTimeoutAttribute]
    public class ForgotPasswordController : Controller
    {

        private readonly IAccountManager _AccountManager;
        public IUserManager _UserManager { get; }

        public ForgotPasswordController(IAccountManager AccountManager, IUserManager UserManager)
        {
            _AccountManager = AccountManager;
            _UserManager = UserManager;
        }

        public async Task<IActionResult> IndexAsync(string i)
        {
            ViewBag.SucessMessage = false;

            var idUserDesencriptado = _AccountManager.FindIdUser(i);

            SM_USER User = await _UserManager.FindById(int.Parse(idUserDesencriptado));
            if (User != null)
            {
                if (DateTime.Now > User.DT_ValidDatePasswordRecoveryLink) {
                    ViewBag.SucessMessage = false;
                    ViewBag.ErrorMessage = "The link used is no longer valid to change your password.Please request a new link.";
                    return View();
                }
            }
            else
            {
                ViewBag.SucessMessage = false;
                ViewBag.ErrorMessage = "Verify that the link sent via email to change your password is correct";
                return View();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string i,AuthenticateModel Autentication)
        {
            try
            {
                if (Autentication.Password == null || Autentication.Password.Equals("")) {
                    ViewBag.SucessMessage = false;
                    ViewBag.ErrorMessage = "Please enter a valid password";
                    return View();
                }

                var idUserDesencriptado = _AccountManager.FindIdUser(i);

                SM_USER User = await _UserManager.FindById(int.Parse(idUserDesencriptado));
                if (User != null)
                {
                    SM_USER user = await _UserManager.Save(new SM_USER
                    {
                        ID_User = User.ID_User,
                        TX_Email = User.TX_Email,
                        TX_FirstName = User.TX_FirstName,
                        TX_SecondName = User.TX_SecondName,
                        TX_LastName = User.TX_LastName,
                        TX_SecondLastName = User.TX_SecondLastName,
                        TX_Phone = User.TX_Phone,
                        BO_Active = User.BO_Active,
                        TX_Password = Autentication.Password,
                        BO_PasswordExpired = false,
                        PasswordChanged = true,
                        DT_ValidDatePasswordRecoveryLink = DateTime.Now
                    });
                    ViewBag.SucessMessage = true;
                    return View();
                }
                else
                {
                    ViewBag.SucessMessage = false;
                    ViewBag.ErrorMessage = "Verify that the link sent via email to change your password is correct";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.SucessMessage = false;
                ViewBag.ErrorMessage = "An error has occurred: Verify that you are at the correct link";
                return View();
            }
        }




    }
}