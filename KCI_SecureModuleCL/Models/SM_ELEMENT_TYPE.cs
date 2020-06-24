using System;
using System.Collections.Generic;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_ELEMENT_TYPE
    {
        public SM_ELEMENT_TYPE()
        {
            SM_ELEMENT = new HashSet<SM_ELEMENT>();
        }

        public int ID_Type { get; set; }
        public string TX_Type { get; set; }

        public virtual ICollection<SM_ELEMENT> SM_ELEMENT { get; set; }
    }
}
