using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using Qmos.Data;
using Qmos.Data.Repositories.Abstractions;
using Qmos.Entities;
using Qmos.Entities.Enums;
using Qmos.Manager.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace Qmos.Manager
{
    public class UpdateTimeManager : ManagerBase, IUpdateTimeManager
    {
        public IUpdateTimeManager Repository { get; set; }
        public UpdateTimeManager(IUpdateTimeManager repository,
            ILoggerErrorManager loggerErrorManager,
            ILoggerActionsManager logger_ActionsManager)
            : base(loggerErrorManager, logger_ActionsManager)
        {
            Repository = repository;
        }

        public async Task<IList<UpdateTime>> All(bool onlyActives = true)
        {
            try
            {
                IList<UpdateTime> list = await Repository.All();
                if (!onlyActives)
                {
                    return list.OrderBy(c => c.Name).ToList();
                }
                return list.Where(c => c.Active).OrderBy(o => o.Name).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }
    }
}
