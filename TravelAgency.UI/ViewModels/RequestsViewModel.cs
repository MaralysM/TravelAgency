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
    public class RequestsViewModel
    {
        public int ID_Requests { get; set; }
        public Travellers Travellers { get; set; }
        public Travels Travels { get; set; }
        public List<SelectListItem> TravelsList { get; set; }
        public List<SelectListItem> TravellersList { get; set; }
        public IEnumerable<Requests> Requests { get; set; }
    }
}
