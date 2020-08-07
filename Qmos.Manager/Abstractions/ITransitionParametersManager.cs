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
        //Task Save(UpdateTime updateTime);
        //Task<UpdateTime> FindById(object id);
        //Task Delete(short id);
    }
}
