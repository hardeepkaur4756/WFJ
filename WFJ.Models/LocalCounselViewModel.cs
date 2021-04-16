using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class LocalCounselFilterViewModel
    {
        public List<SelectListItem> states { get; set; }
        public List<SelectListItem> countries { get; set; }
        public string FormType { get; set; }
    }

    public class LocalCounselViewModel : ExceptionModel
    {
        public LocalCounselFilterViewModel localCounselFilterViewModel { get; set; }
    }
}
