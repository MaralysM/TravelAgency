using System;
using System.Collections.Generic;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_ROLE
    {
        public SM_ROLE()
        {
            SM_ROLE_ELEMENT = new HashSet<SM_ROLE_ELEMENT>();
            SM_ROLE_USER = new HashSet<SM_ROLE_USER>();
        }

        public int ID_Role { get; set; }
        public string TX_Role { get; set; }
        public string TX_Description { get; set; }
        public int? ID_Element { get; set; }
        public bool BO_VisibleCliente { get; set; }


        public virtual SM_ELEMENT ID_ElementNavigation { get; set; }
        public virtual ICollection<SM_ROLE_ELEMENT> SM_ROLE_ELEMENT { get; set; }
        public virtual ICollection<SM_ROLE_USER> SM_ROLE_USER { get; set; }
    }
}
