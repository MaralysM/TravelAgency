
using KCI_SecureModuleCL.Models;
using TravelAgency.Data;
using TravelAgency.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Manager
{

    public interface ITravellersManager
    {
        Task Save(Travellers entity);
        Task<IEnumerable<Travellers>> All();
        Task Delete(int id);
        Task<Travellers> FindById(int id);
    }
}
