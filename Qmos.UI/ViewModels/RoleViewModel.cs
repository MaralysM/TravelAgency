using KCI_SecureModuleCL.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Qmos.UI.ViewModels
{
    public class RoleViewModel
    {
        public int ID_Role { get; set; }
        [Required]
        public string TX_Role { get; set; }
        public string TX_Description { get; set; }
        [Required]
        public int ID_Element { get; set; }
        [Required]
        public bool BO_VisibleCliente { get; set; }
        public IEnumerable<SelectListItem> ListAplications { get; set; }

        public string RoleTreeView { get; set; }

        public string ID_Elements { get; set; }

    }
}
