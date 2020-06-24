using KCI_SecureModuleCL.Models;
using KCI_SecureModuleCL.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmos.UI.Models
{
    public class TreeBuilder
    {
        private static int Id = 0;

        public static TreeResult GenerateTree(IEnumerable<SM_ELEMENT> TreeActual,
            StringBuilder TreeString, bool isFirstCall = true)
        {
            if (TreeActual.Count() > 0)
            {
                foreach (var MenuItem in TreeActual)
                {
                    bool HasSubMenus = (MenuItem.SubMenus.Count > 0);

                    TreeString.Append(String.Format(@"<li class=""dd-item"" data-id=""{0}"">", Id));
                    if (HasSubMenus)
                    {
                        TreeString.Append(String.Format(@"<button class=""dd-collapse"" data-action=""collapse"" type=""button"">Collapse</button>"));
                        TreeString.Append(String.Format(@"<button class=""dd-expand"" data-action=""expand"" type=""button"">Expand</button>"));

                    }
                    TreeString.Append(String.Format(@"<div class=""dd-handle dd-nodrag"">"));
                    if (MenuItem.TX_Icon != null && !MenuItem.TX_Icon.Equals(""))
                    {
                        TreeString.Append(String.Format(@"<span class=""label {0}"">", GetColorLabel(MenuItem.ID_Type)));
                        TreeString.Append(String.Format(@"<i class=""{0}""></i>", MenuItem.TX_Icon));
                        TreeString.Append(String.Format(@"</span>"));
                    }
                    TreeString.Append(String.Format(@"{0}&nbsp;&nbsp;&nbsp;", MenuItem.TX_Name));
                    TreeString.Append(String.Format(@"<input {2} class=""chkElements"" data-val=""{1}"" id=""{0}"" name=""{0}"" type=""checkbox"" value=""{1}"" data-parent=""{3}"">", MenuItem.ID_Element, MenuItem.BO_Authorized, ((MenuItem.BO_Authorized) ? "checked" : ""), MenuItem.ID_ElementParent));
                    //TreeString.Append(String.Format(@"<input name=""{0}"" type=""hidden"" value=""{1}"">", MenuItem.ID_Element, MenuItem.BO_Authorized));
                    TreeString.Append(String.Format(@"</div>"));
                    //TreeString.Append(String.Format(@"<input name=""{0}"" type=""hidden"" value=""{1}"">", MenuItem.ID_Element, MenuItem.BO_Authorized));
                    //TreeString.Append(String.Format(@"<input name=""{0}"" type=""hidden"" value=""{1}"">", MenuItem.ID_Element, MenuItem.BO_Authorized));
                    //TreeString.Append(String.Format(@"<input name=""{0}"" type=""hidden"" value=""{1}"">", MenuItem.ID_Element, MenuItem.BO_Authorized));
                    if (HasSubMenus)
                    {
                        TreeString.Append(String.Format(@"<ol class=""dd-list"">"));
                        TreeString.Append(GenerateTree(MenuItem.SubMenus, new StringBuilder(), false).Tree);
                        TreeString.Append(String.Format(@"</ol>"));
                    }
                    TreeString.Append(String.Format(@"</li>"));
                    Id++;
                }
            }
            return new TreeResult { Tree = TreeString.ToString() };
        }
        private static string GetColorLabel(int ID_Type)
        {
            switch (ID_Type)
            {
                case ElementType.Menu:
                    return "label-info";
                case ElementType.Application:
                    return "label-success";
                case ElementType.Element:
                    return "label-danger";
                default:
                    return "label-warning";
            }
        }
    }
    public class TreeResult
    {
        public string Tree { get; set; }
    }
}
