using KCI_SecureModuleCL.Models;
using Qmos.Data;
using Qmos.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Qmos.Manager
{

    public interface IRoleManager
    {
        //ICompanyRepository Repository { get; set; }

        Task Save(SM_ROLE role);
        Task<IEnumerable<SM_ROLE>> All();
        Task Delete(int id);
        Task<SM_ROLE> FindById(object id);

        Task<IEnumerable<SM_ELEMENT>> AllApplications();

        Task<IEnumerable<SM_ELEMENT>> GetTreeView(int ID_Role);

        Task<bool> UpdateRolesElements(int ID_Role, int[] ID_Elements);



    }
}
