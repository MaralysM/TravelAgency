using KCI_SecureModuleCL.Models;
using TravelAgency.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TravelAgency.UI.ViewModels
{
    public class UserViewModel
    {
        public int ID_User { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "El primer nombre es requerido")]
        [RegularExpression(@"[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+", ErrorMessage = "El primer nombre debe contener únicamente letras")]
        public string TX_FirstName { get; set; }
        [StringLength(20)]
        [RegularExpression(@"[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+", ErrorMessage = "El segundo nombre debe contener únicamente letras")]
        public string TX_SecondName { get; set; }
        [StringLength(20)]
        [Required(ErrorMessage = "El primer apellido es requerido")]
        [RegularExpression(@"[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+", ErrorMessage = "El primer apellido debe contener únicamente letras")]
        public string TX_LastName { get; set; }
        [StringLength(20)]
        [RegularExpression(@"[a-zA-ZñÑáéíóúÁÉÍÓÚ\s]+", ErrorMessage = "El segundo apellido debe contener únicamente letras")]
        public string TX_SecondLastName { get; set; }
        public string TX_Email { get; set; }
        [StringLength(20)]
        [RegularExpression(@"([0-9]{7,20})", ErrorMessage = "El número telefónico debe contener un mínimo de 7 y un máximo de 20 dígitos únicamente númericos")]
        public string TX_Phone { get; set; }
        public string TX_Password { get; set; }
    }
}
