using System.ComponentModel.DataAnnotations.Schema;

namespace KCI_SecureModuleCL.Models
{
    public partial class SM_USER
    {
        [NotMapped]
        public string Token { get; set; }
        [NotMapped]
        public SM_ROLE CurrentRole { get; set; }
        [NotMapped]
        public bool PasswordChanged { get; set; }
    }
}
