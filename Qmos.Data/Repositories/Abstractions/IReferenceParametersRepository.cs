using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface IReferenceParametersRepository
    {
        Task<IList<ReferenceParameters>> AllAsync();
        bool UpdateReference(ReferenceParameters entity);
        List<ReferenceParameters> FindByIdElement(int id_element);
        short Save(ReferenceParameters entity, int id_average);
        void Remove(short id);
        Task<IList<ChildElement>> AllChildElement();
    }
}
