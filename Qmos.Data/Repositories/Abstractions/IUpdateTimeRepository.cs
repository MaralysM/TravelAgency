using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface IUpdateTimeRepository
    {
        void Add(LoggerError dataObject);
        Task<IList<UpdateTime>> AllAsync();
    }
}
