using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DomainEvents.Helpers.Selects
{
    public static class SelectHelper
    {

      
        public static IEnumerable<SelectListItem> GetItems(params string[] values)
        {
            var selectListItems = new List<SelectListItem>();

            foreach (var item in values)
            {
                var selectItem = new SelectListItem
                {
                    Text = item,
                    Value = item
                };

                selectListItems.Add(selectItem);
            }

            return selectListItems;
        }


        // hello=merhaba.
        public static IEnumerable<SelectListItem> GetItems(params KeyValuePair<string,int>[] values)
        {
            var selectListItems = new List<SelectListItem>();
            selectListItems.Add(new SelectListItem { Text="Seçiniz",Value="-1" });

            foreach(var item in values)
            {
                var selectItem = new SelectListItem
                {
                    Text = item.Key,
                    Value = item.Value.ToString()
                };

                selectListItems.Add(selectItem);
            }

            return selectListItems;
        }



    }
}
