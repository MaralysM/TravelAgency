using KCI_SecureModuleCL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qmos.UI.Models
{
    public class MenuBuilder
    {

        public static MenuResult GenerateMenu(IEnumerable<SM_ELEMENT> MenuActual,
            StringBuilder MenuString, bool IsFirstCall = false)
        {
            MenuActual = MenuActual.OrderBy(c => c.ID_ElementParent);
            string DefaultUrl = "";
            if (MenuActual.Count() > 0)
            {
                int Element = 0;

                foreach (var MenuItem in MenuActual)
                {
                    bool HasSubMenus = (MenuItem.SubMenus.Count > 0);

                    string TX_Url = MenuItem.TX_Url == null ? "#" : MenuItem.TX_Url;
                    string TX_Name = MenuItem.TX_Name;
                    string TX_Icon = MenuItem.TX_Icon == null ? "" : MenuItem.TX_Icon.ToString();



                    if (!HasSubMenus)
                    {
                        //if (url != "#")
                        //{
                        //string Linea = String.Format(@"<li><a href=""{0}""><i class=""{2}""></i> <span>{1}</span></a></li>", TX_Url, TX_Name, TX_Icon);

                        if (Element == 0 && IsFirstCall)
                        {
                            DefaultUrl = MenuItem.TX_Url;
                            MenuString.Append(String.Format(@"<li class=""active""><a href=""{0}""><i class=""{1}""></i><span class=""nav-label"">{2}</span></a></li>", TX_Url, TX_Icon, TX_Name));
                        }
                        else
                        {
                            MenuString.Append(String.Format(@"<li><a href=""{0}""><i class=""{1}""></i><span class=""nav-label"">{2}</span></a></li>", TX_Url, TX_Icon, TX_Name));
                        }

                    }
                    else
                    {
                        //}
                        if (Element == 0 && IsFirstCall)
                        {
                            DefaultUrl = MenuItem.TX_Url;
                            MenuString.Append(@"<li class=""active"">");
                        }
                        else
                        {
                            MenuString.Append("<li>");
                        }

                        MenuString.Append(String.Format(@"<a href=""{0}""><i class=""{1}""></i><span class=""nav-label"">{2}</span><span class=""fa arrow""></span></a>", TX_Url, TX_Icon, TX_Name));

                        string ID_Element = MenuItem.ID_Element.ToString();
                        string ID_ElementParent = MenuItem.ID_ElementParent.ToString();

                        //var SubMenus = MenuItem.SubMenus;
                        if (MenuItem.SubMenus.Count > 0 && !ID_Element.Equals(ID_ElementParent))
                        {

                            MenuString.AppendLine(String.Format(@"<ul class=""nav nav-second-level collapse"">", TX_Icon, TX_Name));
                            MenuResult MenuX = GenerateMenu(MenuItem.SubMenus, new StringBuilder());
                            
                            MenuString.Append(MenuX.Menu);
                            MenuString.Append("</ul>");
                        }
                        MenuString.Append("</li>");
                    }
                    Element++;
                }
            }
            return new MenuResult { Menu = MenuString.ToString()};
        }

    }

    public class MenuResult
    {
        public string Menu { get; set; }
        //public string DefaultUrl { get; set; }
    }
}
