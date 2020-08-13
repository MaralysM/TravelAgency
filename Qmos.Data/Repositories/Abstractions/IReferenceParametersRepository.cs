using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface IReferenceParametersRepository
    {
        Task<IList<ReferenceParameters>> AllAsync();
        bool UpdateReference(ReferenceParameters entity);
        ReferenceParameters FindByIdElement(int id_element);

    }
}
