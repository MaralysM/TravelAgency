
using KCI_SecureModuleCL.Models;
using TravelAgency.Data;
using TravelAgency.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Manager
{

    public interface IRequestsManager
    {
        Task Save(Requests entity);
        Task<IEnumerable<Requests>> All();
        Task Delete(int id);
        Task<Requests> FindById(int id);
    }
}
