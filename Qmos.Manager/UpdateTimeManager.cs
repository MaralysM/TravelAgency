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
    public class UpdateTimeManager : ManagerBase, IUpdateTimeManager
    {
        public IUpdateTimeRepository Repository { get; set; }
        public UpdateTimeManager(IUpdateTimeRepository repository,
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
                IList<UpdateTime> list = await Repository.AllAsync();
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

        public async Task Save(UpdateTime updateTime)
        {
            try
            {
                if (updateTime.Id == 0)
                {
                    await Repository.Add(updateTime);
                }
                else
                {
                    await Repository.UpdateAsync(updateTime, updateTime.Id);
                }
            }
            catch (UniqueKeyException ex)
            {
                throw new Exception("Cannot insert or update a value duplicate");
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot add a register");
            }
        }


        public async Task<UpdateTime> FindById(object id)
        {
            try
            {
                return await Repository.FindByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to search");
            }
        }

        public async Task Delete(short id)
        {
            try
            {
                UpdateTime entity = await Repository.FindByIdAsync(id);
                Repository.Remove(entity);
            }
            catch (DeleteWithRelationshipException ex)
            {
                throw new Exception("The record you are trying to delete is related to another");
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to delete");
            }
        }

    }
}
