
using KCI_SecureModuleCL.Models;
using TravelAgency.Data;
using TravelAgency.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Manager
{

    public interface ITravelsManager
    {
        Task Save(Travels entity);
        Task<IEnumerable<Travels>> All();
        Task Delete(int id);
        Task<Travels> FindById(int id);
    }
}
