using TravelAgency.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TravelAgency.Data
{
    public interface IRequestsRepository
    {
        Task<IList<Requests>> AllAsync();
        Task<Requests> UpdateAsync(Requests entity, params object[] Id);
        Task<Requests> FindByIdAsync(params object[] values);
        void Remove(Requests entity);
        Task Add(Requests entity);

    }
}
