using Qmos.Data;
using Qmos.Entities;

namespace Qmos.Manager
{
   public interface ILoggerActionsManager
    {
        ILogger_actionsRepository Repository { get; set; }

        void Add(LoggerActions logger_Actions);
    }

}
