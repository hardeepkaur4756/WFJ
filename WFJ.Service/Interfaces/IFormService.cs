using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IFormService
    {
        List<SelectListItem> GetAllForms();
        ManagePlacementsModel GetPlacements(int clientId, int formTypeId, string searchKeyword, DataTablesParam param, string sortDir, string sortCol, int pageNo, int? userId);
    }
}
