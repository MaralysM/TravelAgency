using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface IUpdateTimeRepository
    {
        Task Add(UpdateTime entity);
        Task<IList<UpdateTime>> AllAsync();
        Task<UpdateTime> UpdateAsync(UpdateTime entity, params object[] Id);
        Task<UpdateTime> FindByIdAsync(params object[] values);
        void Remove(UpdateTime entity);

    }
}
