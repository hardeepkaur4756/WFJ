using System.Collections.Generic;
using System.Web.Mvc;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IStatusCodesService
    {
        List<SelectListItem> GetByFormID(int FormID);
        StatusCodesModel GetByStatusCodeAndFormId(int statusCode, int formId);
    }
}
