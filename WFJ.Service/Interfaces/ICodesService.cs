using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Service.Interfaces
{
    public interface ICodesService
    {
        List<SelectListItem> GetAllByType(string type);
        List<SelectListItem> GetAllStateByType(string type);
    }
}
