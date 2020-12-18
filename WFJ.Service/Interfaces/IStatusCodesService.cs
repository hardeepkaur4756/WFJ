using System.Collections.Generic;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
    public interface IStatusCodesService
    {
        List<SelectListItem> GetByFormID(int FormID);
        StatusCodesModel GetByStatusCodeAndFormId(int statusCode, int formId);
        List<StatusCodesModel> GetModelByFormID(int formId);
    }
}
