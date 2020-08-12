using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface IReferenceParametersRepository
    {
        Task<IList<ReferenceParameters>> AllAsync();

    }
}
