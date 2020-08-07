using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface ITransitionParametersRepository
    {
        short AddHeader(TransitionParametersHeader entity);
        bool AddDetail(long idHeader, TransitionParametersHeader entity);
        Task<IList<TransitionParametersHeader>> AllAsync();
        Task<UpdateTime> UpdateAsync(UpdateTime entity, params object[] Id);
        Task<UpdateTime> FindByIdAsync(params object[] values);
        void Remove(UpdateTime entity);

    }
}
