using Qmos.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Qmos.UI.ViewModels
{
    public class ReferenceParametersViewModel
    {
        public int idElement { get; set; }
        public short idChild { get; set; }
        public string Reference{ get; set; }
        public string RefMin{ get; set; }
        public string RefMax{ get; set; }
        public int idAverage { get; set; }
        public IEnumerable<SelectListItem> ElementList { get; set; }
        public IEnumerable<SelectListItem> ChildList { get; set; }
        public IList<ReferenceParameters> List { get; set; }
        public ReferenceParametersViewModel()
        {
            List = new List<ReferenceParameters>();
        }

    }
}
