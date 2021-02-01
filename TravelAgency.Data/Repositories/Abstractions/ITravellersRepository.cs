using TravelAgency.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
    public interface ITravellersRepository
    {
        Task<IList<Travellers>> AllAsync();
        Task<Travellers> UpdateAsync(Travellers entity, params object[] Id);
        Task<Travellers> FindByIdAsync(params object[] values);
        void Remove(Travellers entity);
        Task Add(Travellers entity);

    }
}
