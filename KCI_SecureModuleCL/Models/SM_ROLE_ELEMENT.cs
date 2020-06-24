using System;
using System.Collections.Generic;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_ROLE_ELEMENT
    {
        public int ID_RoleElement { get; set; }
        public int ID_Role { get; set; }
        public int ID_Element { get; set; }

        public virtual SM_ELEMENT ID_ElementNavigation { get; set; }
        public virtual SM_ROLE ID_RoleNavigation { get; set; }
    }
}
