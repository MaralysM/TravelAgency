
namespace Qmos.Manager.Abstractions
{
    public abstract class ManagerBase
    {
        public ILoggerErrorManager LoggerErrorManager { get; }
        public ILoggerActionsManager LoggerActionsManager { get; }
        public IUserManager UserManager { get; }


        public ManagerBase(ILoggerErrorManager loggerErrorManager, ILoggerActionsManager logger_ActionsManager, IUserManager userManager = null)
        {
            LoggerErrorManager = loggerErrorManager;
            LoggerActionsManager = logger_ActionsManager;
            UserManager = userManager;
        }
    }
}
