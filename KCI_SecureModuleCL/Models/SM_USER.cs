using System;
using System.Collections.Generic;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_USER
    {
        public SM_USER()
        {
            SM_ROLE_USER = new HashSet<SM_ROLE_USER>();
        }

        public int ID_User { get; set; }
        public string TX_FirstName { get; set; }
        public string TX_SecondName { get; set; }
        public string TX_LastName { get; set; }
        public string TX_SecondLastName { get; set; }
        public string TX_Phone { get; set; }
        public string TX_Email { get; set; }
        public string TX_Password { get; set; }
        public bool BO_Active { get; set; }
        public bool BO_PasswordExpired { get; set; }
        public string TX_Link { get; set; }
        public int? ID_PriceList { get; set; }
        public DateTime? DT_ValidDatePasswordRecoveryLink { get; set; }

        public virtual ICollection<SM_ROLE_USER> SM_ROLE_USER { get; set; }
    }
}
