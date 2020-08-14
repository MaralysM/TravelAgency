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
    public class ElementManager : ManagerBase, IElementManager
    {

        public IElementService ElementService;

        public ElementManager(IElementService elementService,
                          ILoggerErrorManager loggerErrorManager,
                          ILoggerActionsManager logger_ActionsManager) : base(loggerErrorManager, logger_ActionsManager)
        {
            ElementService = elementService;
        }

        public async Task Save(SM_ELEMENT element)
        {
            try
            {
                if (element.ID_Element == 0)
                {
                    ElementService.Create(new SM_ELEMENT { ID_ElementParent = element.ID_ElementParent, ID_Type = element.ID_Type, TX_Icon = element.TX_Icon, TX_Name = element.TX_Name, TX_Url = element.TX_Url });
                    LoggerActionsManager.Add(new LoggerActions { TypeAction = TypeActions.Insert, Message = "Register has inserted succesfull-Element", UserId = 0 });
                }
                else
                {
                    ElementService.Edit(element);
                    LoggerActionsManager.Add(new LoggerActions { TypeAction = TypeActions.Update, Message = "Register has modify succesfull-Element", UserId = 0 });

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
                var obj =  ElementService.GetById(id);
                ElementService.Delete(id);

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

        public async Task<SM_ELEMENT> FindById(object id)
        {
            try
            {
                return ElementService.GetById((int)id);
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to search");
            }
        }

        public async Task<IEnumerable<SM_ELEMENT>> All()
        {
            try
            {
                return ElementService.GetAll();
            }
            catch (Exception ex)
            {
                LoggerErrorManager.Error(ex.Message);
                throw new Exception("Have ocurred an error to get to list");
            }
        }

        public async Task<IEnumerable<SM_ELEMENT_TYPE>> AllTypes()
        {
            return ElementService.GetAllTypes();
        }


        public string[] GetElementsNotAuthorized(int ID_Role)
        {
            return ElementService.GetElementsNotAuthorized(ID_Role);
        }

        public bool RegisterElements(IList<SM_ELEMENT> Elements)
        {
            return ElementService.RegisterElements(Elements);
        }
    }


}
