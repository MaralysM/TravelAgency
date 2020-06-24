using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Services;
using Qmos.Data;
using Qmos.Entities;
using Qmos.Entities.Enums;
using Qmos.Manager.Abstractions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Manager
{
    public class RoleManager : ManagerBase, IRoleManager
    {

        public IRoleService RoleService;
        public IElementService ElementService;

        public RoleManager(IRoleService roleService, IElementService elementService,
                          ILoggerErrorManager loggerErrorManager,
                          ILoggerActionsManager logger_ActionsManager) : base(loggerErrorManager, logger_ActionsManager)
        {
            RoleService = roleService;
            ElementService = elementService;
        }

        public async Task Save(SM_ROLE role)
        {
            try
            {
                if (role.ID_Role == 0)
                {
                    RoleService.Create(role);
                    LoggerActionsManager.Add(new LoggerActions { TypeAction = TypeActions.Insert, Message = "Register has inserted succesfull-Role", UserId = 0 });
                }
                else
                {
                    RoleService.Edit(role);
                    LoggerActionsManager.Add(new LoggerActions { TypeAction = TypeActions.Update, Message = "Register has modify succesfull-Role", UserId = 0 });
                }
            }
            catch (UniqueKeyException ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Cannont insert or update a value duplicate");
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Cannont add a register");
            }
        }

        public async Task Delete(int id)
        {
            try
            {
                var obj = RoleService.Delete(id);
            }
            catch (DeleteWithRelationshipException ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("The record you are trying to delete is related to another");
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to delete");
            }
        }

        public async Task<SM_ROLE> FindById(object id)
        {
            try
            {
                return RoleService.GetById((int)id);
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to search");
            }
        }

        public async Task<IEnumerable<SM_ROLE>> All()
        {
            try
            {
                return RoleService.GetAll();
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to get to list");
            }
        }

       

        public async Task<IEnumerable<SM_ELEMENT>> AllApplications()
        {
            return RoleService.GetApplications();
        }

        public async Task<IEnumerable<SM_ELEMENT>> GetTreeView(int ID_Role)
        {
            return ElementService.GetTreeView(ID_Role);
        }

        public async Task<bool> UpdateRolesElements(int ID_Role, int[] ID_Elements)
        {
            return await ElementService.UpdateRolesElements(ID_Role, ID_Elements);
        }

        public Task<IEnumerable<SM_ROLE>> GetRolesByApplicationAndVisibleCliente(int ID_Application)
        {
            throw new NotImplementedException();
        }
    }


}
