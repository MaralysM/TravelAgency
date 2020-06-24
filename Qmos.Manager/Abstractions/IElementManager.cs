using KCI_SecureModuleCL.Models;
using Qmos.Data;
using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Manager
{

    public interface IElementManager
    {
        Task Save(SM_ELEMENT company);
        Task<IEnumerable<SM_ELEMENT>> All();
        Task Delete(int id);
        Task<SM_ELEMENT> FindById(object id);
        Task<IEnumerable<SM_ELEMENT_TYPE>> AllTypes();

        string[] GetElementsNotAuthorized(int ID_Role);

        bool RegisterElements(IList<SM_ELEMENT> Elements);
    }
}
