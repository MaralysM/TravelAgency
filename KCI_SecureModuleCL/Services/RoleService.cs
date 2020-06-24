using KCI_SecureModuleCL.Helpers;
using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;

namespace KCI_SecureModuleCL.Services
{

    public interface IRoleService
    {

        IEnumerable<SM_ROLE> GetAll();

        SM_ROLE GetById(int id);

        bool Create(SM_ROLE role);

        bool Edit(SM_ROLE role);

        bool Delete(int id);

        IEnumerable<SM_ELEMENT> GetApplications();

        IEnumerable<SM_ROLE> GetRolesByApplication(int ID_Application);

        IEnumerable<SM_ROLE> GetRolesByApplicationAndVisibleCliente(int ID_Application);


    }

    public class RoleService : IRoleService

    {
        private readonly Security_ModuleContext DB;

        private readonly AppSettings _appSettings;

        public RoleService(IOptions<AppSettings> appSettings, Security_ModuleContext _DB)
        {
            _appSettings = appSettings.Value;
            DB = _DB;
        }

        public IEnumerable<SM_ROLE> GetAll()
        {
            return DB.SM_ROLE.Include(x=>x.ID_ElementNavigation).ToList();
        }

        public SM_ROLE GetById(int id)
        {
            var role = DB.SM_ROLE.FirstOrDefault(x => x.ID_Role == id);
            return role;
        }

        public bool Create(SM_ROLE role)
        {
            try
            {
                DB.SM_ROLE.Add(role);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Edit(SM_ROLE role)
        {
            try
            {

                SM_ROLE editedRole = DB.SM_ROLE.SingleOrDefault(u => u.ID_Role == role.ID_Role);

                editedRole.TX_Description = role.TX_Description;
                editedRole.TX_Role = role.TX_Role;
                editedRole.BO_VisibleCliente = role.BO_VisibleCliente;
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Delete(int id)
        {
            try
            {
                var Elements = DB.SM_ROLE_ELEMENT.Where(r => r.ID_Role == id);
                DB.SM_ROLE_ELEMENT.RemoveRange(Elements);
                DB.SaveChanges();

                var Users = DB.SM_ROLE_USER.Where(r => r.ID_Role == id);
                DB.SM_ROLE_USER.RemoveRange(Users);
                DB.SaveChanges();

                var role = DB.SM_ROLE.FirstOrDefault(x => x.ID_Role == id);
                DB.SM_ROLE.Remove(role);
                DB.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }


        }

        public IEnumerable<SM_ELEMENT> GetApplications()
        {
            return DB.SM_ELEMENT.Where(e=>e.ID_Type == ElementType.Application).OrderBy(e=> e.TX_Name);
        }
        public IEnumerable<SM_ROLE> GetRolesByApplication(int ID_Application)
        {
            return DB.SM_ROLE.Where(e => e.ID_Element == ID_Application).OrderBy(e => e.TX_Role);
        }

        public IEnumerable<SM_ROLE> GetRolesByApplicationAndVisibleCliente(int ID_Application)
        {
            return DB.SM_ROLE.Where(e => e.ID_Element == ID_Application && e.BO_VisibleCliente).OrderBy(e => e.TX_Role);
        }
    }

}

