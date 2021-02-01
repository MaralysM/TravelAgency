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
    public class TravelsViewModel
    {
        public int ID_Travels { get; set; }
        public long NU_TravelCode { get; set; }
        public int NU_NumberOfPlace { get; set; }
        public string TX_Destination { get; set; }
        public string TX_Origin { get; set; }
        public decimal NU_Price { get; set; }  
        public IEnumerable<Travels> Travels { get; set; }
    }
}
