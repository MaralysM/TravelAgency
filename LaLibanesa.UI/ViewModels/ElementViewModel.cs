using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Utilities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Qmos.UI.ViewModels
{
    public class ElementViewModel
    {
        public int ID_Element { get; set; }
        public string TX_Name { get; set; }
        public string TX_Icon { get; set; }
        public string TX_Url { get; set; }
        public int ID_ElementParent { get; set; }
        public int ID_Type { get; set; }
        public IEnumerable<SelectListItem> ListElements { get; set; }
        public IEnumerable<SelectListItem> ListTypes { get; set; }

        public ElementViewModel() {
            
        }

        public static List<SelectListItem> Convert(List<SM_ELEMENT> list)
        {
            var Applications = new SelectListGroup { Name = "Applications" };
            var Menus = new SelectListGroup { Name = "Menus" };
            var Elements = new SelectListGroup { Name = "Items" };

            List<SelectListItem> ListElements = new List<SelectListItem>();
            
            foreach (var item in list)
            {
                switch (item.ID_Type)
                {
                    case ElementType.Application:
                        ListElements.Add(new SelectListItem { Value = item.ID_Element.ToString(), Text = item.TX_Name, Group = Applications });
                        break;
                    case ElementType.Menu:
                        ListElements.Add(new SelectListItem { Value = item.ID_Element.ToString(), Text = item.TX_Name, Group = Menus });
                        break;
                    case ElementType.Element:
                        ListElements.Add(new SelectListItem { Value = item.ID_Element.ToString(), Text = item.TX_Name, Group = Elements });
                        break;
                }
            }
            return ListElements;
        }
    }

    
}