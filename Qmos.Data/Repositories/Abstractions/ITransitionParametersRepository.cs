using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface ITransitionParametersRepository
    {
        Task Add(UpdateTime entity);
        Task<IList<TransitionParametersHeader>> AllAsync();
        Task<UpdateTime> UpdateAsync(UpdateTime entity, params object[] Id);
        Task<UpdateTime> FindByIdAsync(params object[] values);
        void Remove(UpdateTime entity);

    }
}
