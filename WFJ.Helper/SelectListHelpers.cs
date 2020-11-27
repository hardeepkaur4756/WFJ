using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Helper
{
    public class DropdownHelpers
    {
        public static List<SelectListItem> PrependALL(List<SelectListItem> DropdownList)
        {
            return DropdownList.Prepend(new SelectListItem { Text = "All", Value = "-1" }).ToList();
        }
    }
}
