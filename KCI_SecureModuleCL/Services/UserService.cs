using KCI_SecureModuleCL.Helpers;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KCI_SecureModuleCL.Services
{
    public interface IUserService
    {
        SM_USER Authenticate(string username, string password, int application, string link);

        IEnumerable<SM_USER> GetAll();

        SM_USER GetById(int id);

        SM_USER Create(SM_USER user);

        bool Edit(SM_USER user);

        bool Delete(int id);
        bool CheckUserEmail(string useremail);

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

        private SM_USER AuthenticateWithUserAndPassword(string useremail, string password, int application)
        {
            try
            {
                var user = DB.SM_USER.SingleOrDefault(x => x.TX_Email.Equals(useremail));

                if (user == null)
                    return null;

                if (!HashHandler.Validate(password, user.TX_Password))
                    return null;

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

        public SM_USER GetById(int id)
        {
            var user = DB.SM_USER.FirstOrDefault(x => x.ID_User == id);
            return user.WithoutPassword();
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

        public SM_USER Authenticate(string useremail, string password, int application, string link)
        {
                return AuthenticateWithUserAndPassword(useremail, password, application);
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