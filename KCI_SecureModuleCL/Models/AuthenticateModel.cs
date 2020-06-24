using System.ComponentModel.DataAnnotations;

namespace KCI_SecureModuleCL.Models
{
    public class AuthenticateModel
    {
        public string Useremail { get; set; }
        public string Password { get; set; }
        public int Application { get; set; }
        public string Link { get; set; }
    }
}