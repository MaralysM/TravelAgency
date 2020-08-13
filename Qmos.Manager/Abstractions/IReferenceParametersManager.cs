using KCI_SecureModuleCL.Models;
using Qmos.Data;
using Qmos.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Manager
{

    public interface IReferenceParametersManager
    {
        Task<IList<ReferenceParameters>> All();
        bool UpdateReference(ReferenceParameters entity);
        ReferenceParameters FindByIdElement(int id_element);
    }
}
