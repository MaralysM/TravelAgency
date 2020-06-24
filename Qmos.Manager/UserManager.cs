using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using Qmos.Data;
using Qmos.Data.Repositories.Abstractions;
using Qmos.Entities;
using Qmos.Entities.Enums;
using Qmos.Manager.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Qmos.Manager
{
    public class UserManager : ManagerBase, IUserManager
    {

        public IRoleService RoleService;
        public IElementService ElementService;
        public IUserService UserService;
        private readonly IServiceScopeFactory ScopeFactory;
        public IEmailSender EmailSender { get; }
        private string BaseUrl;

        public SM_USER UserCurrent { get => FindById(CurrentUserId).Result; }
        public static int CurrentUserId { get; set; }
        public IConfiguration Configuration { get; }

        public UserManager(
            IRoleService roleService,
            IElementService elementService,
            IUserService userService,
            IEmailSender emailSender,
            ILoggerErrorManager loggerErrorManager,
            ILoggerActionsManager logger_ActionsManager, IConfiguration configuration)
            : base(loggerErrorManager, logger_ActionsManager)
        {

            RoleService = roleService;
            ElementService = elementService;
            UserService = userService;
            EmailSender = emailSender;
            Configuration = configuration;
            BaseUrl = Configuration.GetSection("RecoveryUrl").Value;
        }





        public async Task<IEnumerable<SM_ELEMENT>> AllApplications()
        {
            return RoleService.GetApplications();
        }

        public async Task<IEnumerable<SM_ELEMENT>> GetTreeView(int ID_Role)
        {
            return ElementService.GetTreeView(ID_Role);
        }

        public async Task<bool> UpdateRolesElements(int ID_Role, int[] ID_Elements)
        {
            return await ElementService.UpdateRolesElements(ID_Role, ID_Elements);
        }



        public async Task<IEnumerable<SM_USER>> All(string rol, SM_USER user)
        {
            try
            {
                if (rol.Contains("Administrador de Clientes"))
                    return UserService.GetByCustormer(user);
                else
                    return UserService.GetAll();
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to get to list");
            }
        }
        //public async Task<bool> SearchActiveUsers(string rol, string TX_Link)
        //{
        //    try
        //    {
        //        if (TX_Link != null)
        //        {
        //            Customer _customer = await CustomerRepository.GetById(long.Parse(TX_Link));
        //            return UserService.GetActiveUsers(TX_Link) < _customer.ActiveUsers;
        //        }
        //        else
        //        {
        //            return true;
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerErrorManager.Error(ex.Message);
        //        throw new Exception("Have ocurred an error to get to list");
        //    }
        //}
        public async Task<SM_USER> FindById(object id)
        {
            try
            {
                return UserService.GetById((int)id);
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to search");
            }
        }
        //public async Task<Customer> FindCustomerByUser(string idCustomer)
        //{
        //    try
        //    {
        //        return await CustomerRepository.GetById(long.Parse(idCustomer));
        //    }
        //    catch (Exception ex)
        //    {
        //        LoggerErrorManager.Error(ex.Message);
        //        throw new Exception("Have ocurred an error to search");
        //    }
        //}



        public async Task<IEnumerable<SM_ROLE>> GetRolesByApplication(int ID_Application)
        {
            return RoleService.GetRolesByApplication(ID_Application);
        }
        

        public async Task<SM_USER> Save(SM_USER user)
        {
            try
            {
                if (user.ID_User == 0)
                {
                    var userSuccessfully = UserService.Create(user);
                    LoggerActionsManager.Add(new LoggerActions { TypeAction = TypeActions.Insert, Message = "Register has inserted succesfull-User", UserId = 0 });
                    return userSuccessfully;
                }
                else
                {
                    UserService.Edit(user);
                    LoggerActionsManager.Add(new LoggerActions { TypeAction = TypeActions.Update, Message = "Register has modify succesfull-User", UserId = 0 });
                    return user;
                }
            }
            catch (UniqueKeyException ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Cannont insert or update a value duplicate");
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Cannont add a register");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var obj = UserService.Delete(id);
            }
            catch (DeleteWithRelationshipException ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("The record you are trying to delete is related to another");
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to delete");
            }
        }

        public void AddRoleToUser(SM_ROLE_USER userRole)
        {
            UserService.AddRoleToUser(userRole);
        }

        public IEnumerable<SM_ROLE_USER> GetRolesByUser(int iD_User)
        {
            return UserService.GetAllRolesByUser(iD_User);
        }

        public void DeleteRoleByUser(int iD_UserRoleAplication)
        {
            var result = UserService.DeleteRoleByUser(iD_UserRoleAplication);
        }

        public bool CheckUserEmail(string useremail)
        {
            return UserService.CheckUserEmail(useremail);
        }

//        public async Task<bool> UserManagementNotification(int idUser, bool create)
//        {
//            SM_USER user = await FindById(idUser);
//            List<string> Emails = await AddEmail(user, create);

//            string body = create ? "Cuenta creada" : "Cuenta actualizada";

//            foreach (var Email in Emails)
//            {
//                if (Email != null)
//                {
//                    if (Email.ToString() != "")
//                    {
//                        await EmailSender.SendEmailAsync(Email, $" Usuario {user.TX_FirstName} {user.TX_SecondName} {user.TX_LastName} {user.TX_SecondLastName}",
//                        $@" <html>

//<head></head>

//<body style='font-family:sans-serif;width: 100%;text-align: -webkit-center;'>
//    <div>
//        <img src='cid:Header' width='400px'>
//    </div>
//    <div
//        style='width: 398px;margin-top:-5px;padding-top:20px;background-color:white;border-width:1px;border-style:solid;border-color:white;text-align:center;margin-top:-15px;'>
//        <div><img height='80px' src='cid:Icon'></div>
//        <p style='font-size: 14px;'><strong>Bienvenido</strong>, {user.TX_FirstName} {user.TX_LastName}</p>
//        <p style='font-size: 14px;text-align: center;'>El siguiente enlace le permitir&aacute; acceder al portal de
//            <br /><strong>Lince Comercial</strong> para iniciar por primera vez sesi&oacute;n.
//            <br />
//            <br />
//            <span style='font-size: 16px;'>Su nombre de usuario es: </span>
//        </p>
//        <h3>{user.TX_Email}</h3>
//        <div>
//            <button
//                style='background-color: lightseagreen;color: white; border-radius: 25px; width: 200px; border-color: lightseagreen;height: 40px;'><a style='color: white; text-decoration: none;' href='{BaseUrl}' target='_blank'> Acceder
//                al portal</a></button>
//        </div>
//        <p style='text-align: center;'>
//            Usted podr&aacute; acceder ingresando a <br /><a href='http://www.lincecomercial.com'> www.lincecomercial.com </a>
//            en la pesta&ntilde;a <strong>Portal de Clientes</strong>
//        </p>
//        <p style='font-size: 12px;'>Este mensaje ha sido enviado autom&aacute;ticamente por el sistema</p>
//    </div>
//    <div>
//        <img src='cid:Footer' width='400px'>
//    </div>
//</body>

//</html>", true);
//                    }
//                }
//            }

//            return true;
//        }

        //public async Task<List<string>> AddEmail(SM_USER user, bool create)
        //{// Si se crea el usuario: email al usuario, si se activo: email al vendedor y al usuario, si se inactivo solo al vendedor
        //    List<string> Emails = new List<string>();
        //    SalesEmployee vendedor = await SaleEmployee.GetByUser(user.ID_User);


        //    if (create || user.BO_Active)
        //        Emails.Add(user.TX_Email);

        //    if (!create)
        //        Emails.Add(vendedor.Email);

        //    return Emails;
        //}

        public async Task<IEnumerable<SM_ROLE>> GetRolesByApplicationAndValisibleByCliente(int ID_Application)
        {
            return RoleService.GetRolesByApplicationAndVisibleCliente(ID_Application);
        }
        
    }


}
