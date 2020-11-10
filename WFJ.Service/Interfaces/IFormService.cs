using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
    public interface IFormService
    {
        List<SelectListItem> GetAllForms();
        ManagePlacementsModel GetPlacements(int clientId, int formTypeId, string searchKeyword, DataTablesParam param, string sortDir, string sortCol, int pageNo, int? ClientUserId);

        List<FormFieldViewModel> GetFormFieldsByForm(int FormID);
        List<FormSectionViewModel> GetFormSections();
        FormModel GetFormById(int FormID);
        List<SelectListItem> GetRequestorsDropdown(int FormID);
        List<SelectListItem> GetCollectorsDropdown();
        List<SelectListItem> GetPersonnelsDropdown(int FormID);
    }
}
