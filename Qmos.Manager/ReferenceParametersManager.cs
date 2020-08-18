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
    public class ReferenceParametersManager : ManagerBase, IReferenceParametersManager
    {
        public IReferenceParametersRepository Repository { get; set; }
        public ReferenceParametersManager(IReferenceParametersRepository repository,
            ILoggerErrorManager loggerErrorManager,
            ILoggerActionsManager logger_ActionsManager)
            : base(loggerErrorManager, logger_ActionsManager)
        {
            Repository = repository;
        }

        public async Task<IList<ReferenceParameters>> All()
        {
            try
            {
                IList<ReferenceParameters> list = await Repository.AllAsync();

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }

        public async Task<IList<ChildElement>> AllChildElement()
        {
            try
            {
                IList<ChildElement> list = await Repository.AllChildElement();

                return list.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Have ocurred an error to get to list");
            }
        }
        public bool UpdateReference(ReferenceParameters entity)
        {
            try
            {
                bool resp = Repository.UpdateReference(entity);

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

        public List<ReferenceParameters> FindByIdElement(int id_element)
        {
            try
            {
                List<ReferenceParameters> resp = Repository.FindByIdElement(id_element);

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

        public short Save(ReferenceParameters referenceParameters)
        {
            try
            {
                short resp = Repository.Save(referenceParameters);

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

    }
}
