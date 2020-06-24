using System;
using System.Collections.Generic;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_ROLE_USER
    {
        public int ID_UserRoleApplication { get; set; }
        public int ID_User { get; set; }
        public int ID_Role { get; set; }

        public virtual SM_ROLE ID_RoleNavigation { get; set; }
        public virtual SM_USER ID_UserNavigation { get; set; }
    }
}
