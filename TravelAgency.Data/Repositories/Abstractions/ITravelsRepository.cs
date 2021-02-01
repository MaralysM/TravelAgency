using TravelAgency.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
    public interface ITravelsRepository
    {
        Task<IList<Travels>> AllAsync();
        Task<Travels> UpdateAsync(Travels entity, params object[] Id);
        Task<Travels> FindByIdAsync(params object[] values);
        void Remove(Travels entity);
        Task Add(Travels entity);

    }
}
