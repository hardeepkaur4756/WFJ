using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface ICodesService
    {
        List<SelectListItem> GetAllByType(string type);
        List<SelectListItem> GetAllStateByType(string type);
        CodeModel GetById(int id);
        List<SelectListItem> GetAllStateandProvince();
    }
}
