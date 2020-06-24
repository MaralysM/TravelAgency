using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace Qmos.Data
{
  
    public interface ILogger_actionsRepository
    {
        void Add(LoggerActions dataObject);
        Task<IList<LoggerActions>> AllAsync();
    }
}
