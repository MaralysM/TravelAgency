
using KCI_SecureModuleCL.Models;
using TravelAgency.Data;
using TravelAgency.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Manager
{

    public interface IUserManager
    {
        Task<SM_USER> Save(SM_USER user);
        Task<IEnumerable<SM_USER>> All();
        Task Delete(int id);
        Task<SM_USER> FindById(object id);
    }
}
