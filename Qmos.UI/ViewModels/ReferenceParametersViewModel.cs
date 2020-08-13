using Qmos.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Qmos.UI.ViewModels
{
    public class ReferenceParametersViewModel
    {
        public int idElement { get; set; }
        public string Reference{ get; set; }
        public IEnumerable<SelectListItem> ElementList { get; set; }
        public IList<ReferenceParameters> List { get; set; }
        public ReferenceParametersViewModel()
        {
            List = new List<ReferenceParameters>();
        }

    }
}
