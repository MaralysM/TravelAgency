using Qmos.Data;
using Qmos.Entities;
using System;

namespace Qmos.Manager
{
    public class LoggerErrorManager : ILoggerErrorManager
    {
        public ILoggerErrorRepository Repository { get; set; }
        public LoggerErrorManager(ILoggerErrorRepository repository)
        {
            Repository = repository;
        }

        public void Error(string message, User user = null)
        {
            string msg = $"{user?.Name} | {message}".Replace("\'", "\"");
            Repository.Add(new LoggerError { Date = DateTime.Now, Message = msg });
        }
    }
}
