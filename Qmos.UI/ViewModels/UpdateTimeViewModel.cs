using KCI_SecureModuleCL.Models;
using Qmos.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Qmos.UI.ViewModels
{
    public class UpdateTimeViewModel
    {
        public short Id { get; set; }
        public int IdElement { get; set; }
        public string TIME_REFRESH { get; set; }
        public decimal TIMEMILLISECONDS{ get; set; }
        public bool ACTIVE { get; set; }
        public short ORDER_TRANSITION { get; set; }
        public string REFERENCE { get; set; }

    }
}
