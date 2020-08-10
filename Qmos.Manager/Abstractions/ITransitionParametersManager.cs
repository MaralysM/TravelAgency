using KCI_SecureModuleCL.Models;
using Qmos.Data;
using Qmos.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Manager
{

    public interface ITransitionParametersManager
    {
        Task<IList<TransitionParametersHeader>> All(bool onlyActives = true);
        short Save(TransitionParametersHeader entity);
        Task<TransitionParametersHeader> FindById(object id);
        TransitionParametersDetails FindDetailById(short id);
        void Remove(short id);
        void RemoveDetail(short id);
        bool UpdateHeader(TransitionParametersHeader entity);
        //Task Save(UpdateTime updateTime);
        //Task<UpdateTime> FindById(object id);
    }
}
