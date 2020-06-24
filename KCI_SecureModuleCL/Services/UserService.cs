using KCI_SecureModuleCL.Helpers;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TokenHandler = KCI_SecureModuleCL.Utilities.TokenHandler;

namespace KCI_SecureModuleCL.Services
{
    public interface IUserService
    {
        SM_USER Authenticate(string username, string password, int application, string link);

        IEnumerable<SM_USER> GetAll();
        IEnumerable<SM_USER> GetByCustormer(SM_USER user);

        SM_USER GetById(int id);

        SM_USER Create(SM_USER user);

        bool Edit(SM_USER user);

        bool Delete(int id);
        void AddRoleToUser(SM_ROLE_USER userRole);
        IEnumerable<SM_ROLE_USER> GetAllRolesByUser(int ID_User);
        bool DeleteRoleByUser(int iD_UserRoleAplication);
        bool CheckUserEmail(string useremail);
        int GetActiveUsers(string customer);
    }

    public class UserService : IUserService

    {
        private readonly Security_ModuleContext DB;

        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings, Security_ModuleContext _DB)
        {
            _appSettings = appSettings.Value;
            DB = _DB;
        }

        private SM_ROLE GetRoleByUserIdAndApplication(int ID_User, int ID_Application = 0)
        {
            if (ID_Application == 0)
                ID_Application = DB.SM_ELEMENT.SingleOrDefault(a => a.ID_ElementParent == 0).ID_Element;

            var Rol = from roles in DB.SM_ROLE
                      join usuarios_roles in DB.SM_ROLE_USER on roles.ID_Role equals usuarios_roles.ID_Role
                      join usuarios in DB.SM_USER on usuarios_roles.ID_User equals usuarios.ID_User
                      where roles.ID_Element == ID_Application && usuarios.ID_User == ID_User
                      select new SM_ROLE { ID_Role = roles.ID_Role, ID_Element = roles.ID_Element, TX_Description = roles.TX_Description, TX_Role = roles.TX_Role }; ;



            return Rol.First();
        }
        private SM_USER AuthenticateWithLink(string link, int application)
        {
            try
            {
                var user = DB.SM_USER.SingleOrDefault(x => x.TX_Link.Equals(link));
                // return null if user not found
                if (user == null)
                    return null;

                user.CurrentRole = GetRoleByUserIdAndApplication(user.ID_User, application);

                return user.WithoutPassword();

            }
            catch (Exception)
            {
                return null;
            }
        }
        private SM_USER AuthenticateWithUserAndPassword(string useremail, string password, int application)
        {
            try
            {
                var user = DB.SM_USER.SingleOrDefault(x => x.TX_Email.Equals(useremail));

                // return null if user not found
                if (user == null)
                    return null;

                if (!HashHandler.Validate(password, user.TX_Password))
                    return null;

                user.CurrentRole = GetRoleByUserIdAndApplication(user.ID_User, application);

                user.Token = TokenHandler.GenerateToken(user.ID_User.ToString(), user.CurrentRole.TX_Role, _appSettings.Secret);

                return user.WithoutPassword();

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IEnumerable<SM_USER> GetAll()
        {
            return DB.SM_USER.WithoutPasswords();
        }
        public IEnumerable<SM_USER> GetByCustormer(SM_USER user)
        {
            return DB.SM_USER.Where(x => x.TX_Link == user.TX_Link);
        }

        public SM_USER GetById(int id)
        {
            var user = DB.SM_USER.FirstOrDefault(x => x.ID_User == id);
            return user.WithoutPassword();
        }

        public int GetActiveUsers(string customer)
        {
            return DB.SM_USER.Where(x => x.BO_Active && x.TX_Link == customer).Count();
        }

        public SM_USER Create(SM_USER user)
        {
            try
            {
                user.TX_Password = HashHandler.CreateHash(user.TX_Password);

                DB.SM_USER.Add(user);
                DB.SaveChanges();
                return DB.SM_USER.OrderByDescending(x => x.ID_User).FirstOrDefault();
            }
            catch (Exception ex)
            {

                return user;
            }

        }

        public bool Edit(SM_USER user)
        {
            try
            {
                SM_USER editedUser = DB.SM_USER.SingleOrDefault(u => u.ID_User == user.ID_User);

                if (!user.TX_Email.Equals(editedUser.TX_Email))
                    return false;
                editedUser.TX_Email = user.TX_Email;
                editedUser.TX_FirstName = user.TX_FirstName;
                editedUser.TX_SecondName = user.TX_SecondName;
                editedUser.TX_LastName = user.TX_LastName;
                editedUser.TX_SecondLastName = user.TX_SecondLastName;
                editedUser.TX_Phone = user.TX_Phone;
                editedUser.TX_Link = user.TX_Link;
                editedUser.BO_Active = user.BO_Active;
                editedUser.ID_PriceList = user.ID_PriceList;

                if (user.DT_ValidDatePasswordRecoveryLink != null)
                    editedUser.DT_ValidDatePasswordRecoveryLink = user.DT_ValidDatePasswordRecoveryLink;

                if (user.PasswordChanged)
                    editedUser.TX_Password = HashHandler.CreateHash(user.TX_Password);

                DB.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var roles = DB.SM_ROLE_USER.Where(u => u.ID_User == id);
                DB.SM_ROLE_USER.RemoveRange(roles);
                DB.SaveChanges();

                var user = DB.SM_USER.FirstOrDefault(x => x.ID_User == id);
                DB.SM_USER.Remove(user);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public void AddRoleToUser(SM_ROLE_USER userRole)
        {
            var UserRole = DB.SM_ROLE_USER.SingleOrDefault(x => x.ID_User == userRole.ID_User && x.ID_Role == userRole.ID_Role);
            if (UserRole == null)
            {
                DB.SM_ROLE_USER.Add(userRole);
                DB.SaveChanges();
            }
        }

        public IEnumerable<SM_ROLE_USER> GetAllRolesByUser(int ID_User)
        {
            return DB.SM_ROLE_USER.Where(r => r.ID_User == ID_User).Include(x => x.ID_RoleNavigation).Include(x => x.ID_RoleNavigation.ID_ElementNavigation);
        }

        public bool DeleteRoleByUser(int iD_UserRoleAplication)
        {
            var RoleUser = DB.SM_ROLE_USER.SingleOrDefault(x => x.ID_UserRoleApplication == iD_UserRoleAplication);
            if (RoleUser != null)
            {
                DB.SM_ROLE_USER.Remove(RoleUser);
                DB.SaveChanges();
            }
            return true;
        }

        public SM_USER Authenticate(string useremail, string password, int application, string link)
        {
            if (link == null)
            {
                return AuthenticateWithUserAndPassword(useremail, password, application);
            }
            else
            {
                return AuthenticateWithLink(link, application);
            }
        }

        public bool CheckUserEmail(string useremail)
        {
            var user = DB.SM_USER.SingleOrDefault(u => u.TX_Email.Equals(useremail));
            if (user != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}