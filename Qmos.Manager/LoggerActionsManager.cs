using Qmos.Data;
using Qmos.Entities;
using System;

namespace Qmos.Manager
{
       public class LoggerActionsManager : ILoggerActionsManager
    {
        public ILogger_actionsRepository Repository { get; set; }
        public LoggerActionsManager(ILogger_actionsRepository repository)
        {
            Repository = repository;
        }

        public void Add(LoggerActions logger_Actions)
        {
            logger_Actions.Date = DateTime.Now;
            logger_Actions.Time = DateTime.Now;
            logger_Actions.TypeAction = logger_Actions.TypeAction;
            Repository.Add(logger_Actions);
        }
    }


}
