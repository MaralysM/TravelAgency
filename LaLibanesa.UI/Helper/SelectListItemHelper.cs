using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using Qmos.Entities.Enums;
using System.Threading.Tasks;
using System.Linq;

namespace Qmos.UI.Helper
{
    public static class SelectListItemHelper
    {
        public static List<SelectListItem> ToSelectList<T>(this List<T> list, string idPropertyName, string namePropertyName = "Name",
                    string namePropertyCode = "Code", bool withCode = false)
                where T : class, new()
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();

            list.ForEach(item =>
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = withCode ? $"{item.GetType().GetProperty(namePropertyCode).GetValue(item).ToString()} - {item.GetType().GetProperty(namePropertyName).GetValue(item).ToString().ToUpper()}" :
                    item.GetType().GetProperty(namePropertyName).GetValue(item).ToString().ToUpper(),
                    Value = item.GetType().GetProperty(idPropertyName).GetValue(item).ToString()
                });
            });

            return selectListItems;
        }

        public static List<SelectListItem> ToSelectListEnum<T>() where T: Enum
        {
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            Array values = Enum.GetValues(typeof(T));

            foreach (var i in values)
            {
                selectListItems.Add(new SelectListItem
                {
                    Text = EnumExtentions.GetDescription((Enum)i),
                    Value = ((byte)i).ToString()
                });
            }

            return selectListItems;
        }
    }
}
