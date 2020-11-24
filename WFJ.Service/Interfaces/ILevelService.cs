using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
    public interface ILevelService
    {
        List<SelectListItem> GetAllRegions();
        List<SelectListItem> GetRegionsByClientID(int ClientId);
    }
}
