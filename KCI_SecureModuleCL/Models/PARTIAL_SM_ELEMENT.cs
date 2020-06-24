using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_ELEMENT
    {
        [NotMapped]
        public IList<SM_ELEMENT> SubMenus = new List<SM_ELEMENT>();
        [NotMapped]
        public bool BO_Authorized { get; set; }
    }
}
