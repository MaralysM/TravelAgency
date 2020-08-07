using Qmos.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Qmos.UI.ViewModels
{
    public class TransitionParametersViewModel
    {
        public TransitionParametersHeaderViewModel Header { get; set; }
        public TransitionParametersDetailViewModel Detail { get; set; }
        public IList<TransitionParametersDetails> List { get; set; }
        public TransitionParametersViewModel()
        {
            Header = new TransitionParametersHeaderViewModel();
            Detail = new TransitionParametersDetailViewModel();
            List = new List<TransitionParametersDetails>();
        }

        public class TransitionParametersHeaderViewModel
        {
            public short Id { get; set; }
            public string Name { get; set; }
            public bool Active { get; set; }
        }

        public class TransitionParametersDetailViewModel
        {
            public short Id { get; set; }
            public short IdTansitionParametersHeader { get; set; }
            public string TimeTransition { get; set; }
            public short OrderTransition { get; set; }
            public int IdElement { get; set; }
            public IEnumerable<SelectListItem> TansitionParametersHeaderList { get; set; }
            public IEnumerable<SelectListItem> ElementList { get; set; }        
        }

    }
}
