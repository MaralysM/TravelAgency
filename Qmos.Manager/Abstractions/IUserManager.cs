
using KCI_SecureModuleCL.Models;
using Qmos.Data;
using Qmos.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Manager
{

    public interface IUserManager
    {
        SM_USER UserCurrent { get; }
        Task<SM_USER> Save(SM_USER user);
        Task<IEnumerable<SM_USER>> All(string rol, SM_USER user);
        Task Delete(int id);
        Task<SM_USER> FindById(object id);
        Task<IEnumerable<SM_ELEMENT>> AllApplications();
        Task<IEnumerable<SM_ROLE>> GetRolesByApplication(int ID_Application);
        Task<IEnumerable<SM_ROLE>> GetRolesByApplicationAndValisibleByCliente(int ID_Application);
        void AddRoleToUser(SM_ROLE_USER userRole);
        IEnumerable<SM_ROLE_USER> GetRolesByUser(int iD_User);
        void DeleteRoleByUser(int iD_UserRoleAplication);
        bool CheckUserEmail(string username);
    }
}
