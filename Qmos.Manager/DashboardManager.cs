﻿using KCI_SecureModuleCL.Models;
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
    public class DashboardManager : ManagerBase, IDashboardManager
    {
        public IUpdateTimeRepository Repository { get; set; }
        public DashboardManager(IUpdateTimeRepository repository,
            ILoggerErrorManager loggerErrorManager,
            ILoggerActionsManager logger_ActionsManager)
            : base(loggerErrorManager, logger_ActionsManager)
        {
            Repository = repository;
        }

        public async Task<decimal> ConversionToMilliseconds()
        {
            try
            {

                var EntityUpdateTime = await Repository.AllAsync();

                string time = EntityUpdateTime.FirstOrDefault().time_refresh;
                string[] timeArray = time.Split(':');
                var totalH = int.Parse(timeArray[0]) * 3600000;// Conversion Horas
                var TotalM = int.Parse(timeArray[1]) * 60000;//Conversion Minutos
                var TotalS = int.Parse(timeArray[2]) * 1000;// Conversion Segundos

                return (totalH + TotalM + TotalS);
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }
    
    }
}