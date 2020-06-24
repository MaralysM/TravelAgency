using Qmos.Data;
using Qmos.Entities;

namespace Qmos.Manager
{
    public interface ILoggerErrorManager
    {
        ILoggerErrorRepository Repository { get; set; }

        void Error(string message, User user = null);
    }
}