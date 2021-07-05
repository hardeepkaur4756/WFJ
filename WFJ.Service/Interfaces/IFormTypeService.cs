using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
   public interface IFormTypeService
    {
        List<FormTypeModel> GetAll();
        List<SelectListItem> GetFormTypesDropdown(int clientId = 0);
    }
}
