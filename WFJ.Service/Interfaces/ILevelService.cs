using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Service.Interfaces
{
    public interface ILevelService
    {
        List<SelectListItem> GetAllRegions();
    }
}
