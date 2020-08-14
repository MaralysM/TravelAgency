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
        void Remove(short entity);
        Task<TransitionParametersHeader> FindByIdAsync(params object[] values);
        TransitionParametersDetails FindDetailById(short id);
        void RemoveDetail(short id);
        bool UpdateDetail(TransitionParametersHeader entity);
        bool UpdateHeader(TransitionParametersHeader entity);
        Task<TransitionParametersDetails> GetByName(string name);

    }
}
