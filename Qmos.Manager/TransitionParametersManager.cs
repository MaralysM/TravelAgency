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
    public class TransitionParametersManager : ManagerBase, ITransitionParametersManager
    {
        public ITransitionParametersRepository Repository { get; set; }
        public TransitionParametersManager(ITransitionParametersRepository repository,
            ILoggerErrorManager loggerErrorManager,
            ILoggerActionsManager logger_ActionsManager)
            : base(loggerErrorManager, logger_ActionsManager)
        {
            Repository = repository;
        }

        public async Task<IList<TransitionParametersHeader>> All(bool onlyActives = true)
        {
            try
            {
                IList<TransitionParametersHeader> list = await Repository.AllAsync();
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

        public short Save(TransitionParametersHeader entity)
        {
            try
            {
                if (entity.Id == 0)
                {
                    entity.Id = Repository.AddHeader(entity);
                    Repository.AddDetail(entity.Id, entity);
                }
                else
                {
                    Repository.UpdateHeader(entity);
                    if (entity.TransitionParametersDetailsEntity.Id > 0)
                    {
                        Repository.UpdateDetail(entity);
                    }
                    else
                    {
                        Repository.AddDetail(entity.Id, entity);
                    }
                }
                return entity.Id;
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



        public bool UpdateHeader(TransitionParametersHeader entity)
        {
            try
            {
                  bool resp =   Repository.UpdateHeader(entity);
                
                return resp;
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
        public async Task<TransitionParametersHeader> FindById(object id)
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

        public TransitionParametersDetails FindDetailById(short id)
        {
            try
            {
                return Repository.FindDetailById(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to search");
            }
        }

        public void Remove(short id)
        {
            try
            {
                Repository.Remove(id);
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

        public void RemoveDetail(short id)
        {
            try
            {
                Repository.RemoveDetail(id);
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

        public decimal ConversionToMilliseconds(string Time)
        {
            try
            {
                int totalH = 0; int TotalM = 0; int TotalS = 0;
                string[] timeArray = Time.Split(':');
                totalH = int.Parse(timeArray[0]) * 3600000;// Conversion Horas
                TotalM = int.Parse(timeArray[1]) * 60000;//Conversion Minutos
                TotalS = int.Parse(timeArray[2]) * 1000;// Conversion Segundos    
                
                return (totalH + TotalM + TotalS);
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error ");
            }
        }

    }
}
