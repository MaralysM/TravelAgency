﻿using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Data
{
    public interface ILoggerErrorRepository
    {
        void Add(LoggerError dataObject);
        Task<IList<LoggerError>> AllAsync();
    }
}
