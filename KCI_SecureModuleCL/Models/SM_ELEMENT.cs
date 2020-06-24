using System;
using System.Collections.Generic;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_ELEMENT
    {
        public SM_ELEMENT()
        {
            SM_ROLE = new HashSet<SM_ROLE>();
            SM_ROLE_ELEMENT = new HashSet<SM_ROLE_ELEMENT>();
        }

        public int ID_Element { get; set; }
        public string TX_Name { get; set; }
        public string TX_Icon { get; set; }
        public string TX_Url { get; set; }
        public int ID_ElementParent { get; set; }
        public int ID_Type { get; set; }
        public bool BO_Default { get; set; }

        public virtual SM_ELEMENT_TYPE ID_TypeNavigation { get; set; }
        public virtual ICollection<SM_ROLE> SM_ROLE { get; set; }
        public virtual ICollection<SM_ROLE_ELEMENT> SM_ROLE_ELEMENT { get; set; }
    }
}
