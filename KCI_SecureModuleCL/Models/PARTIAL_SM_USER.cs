using System.ComponentModel.DataAnnotations.Schema;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_USER
    {
        [NotMapped]
        public bool PasswordChanged { get; set; }
    }
}
