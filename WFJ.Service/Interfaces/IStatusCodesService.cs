using System.Collections.Generic;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
    public interface IStatusCodesService
    {
        List<SelectListItem> GetByFormID(int FormID, bool SelectActive = false);
        StatusCodesModel GetByStatusCodeAndFormId(int statusCode, int formId);
    }
}
