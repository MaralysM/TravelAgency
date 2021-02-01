using System;
using System.Collections.Generic;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_USER
    {
        public int ID_User { get; set; }
        public string TX_FirstName { get; set; }
        public string TX_SecondName { get; set; }
        public string TX_LastName { get; set; }
        public string TX_SecondLastName { get; set; }
        public string TX_Phone { get; set; }
        public string TX_Email { get; set; }
        public string TX_Password { get; set; }
    }
}
