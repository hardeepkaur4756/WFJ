using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class StatusCodesService:IStatusCodesService
    {
        public IStatusCodesRepository statusCodesRepo = new StatusCodesRepository();

        public List<SelectListItem> GetByFormID(int FormID)//, bool SelectActive = false)
        {
            return statusCodesRepo.GetByFormID(FormID).Where(x => x.Description != null).Select(x => new SelectListItem() { Text = x.Description, Value = x.StatusCode1.ToString() }).ToList();
        }

        public List<StatusCodesModel> GetModelByFormID(int formID)
        {
            return statusCodesRepo.GetByFormID(formID).Where(x => x.Description != null).Select(x => new StatusCodesModel() { Description = x.Description, StatusCode = x.StatusCode1,StatusLevel=x.StatusLevel }).ToList();
        }

        public StatusCodesModel GetByStatusCodeAndFormId(int statusCode, int formId)
        {
            var statusCodes = statusCodesRepo.GetByStatusCodeAndFormId(statusCode, formId);
            return new StatusCodesModel
            {
                ID = statusCodes.ID,
                ClientID = statusCodes.ClientID,
                complianceDuration = statusCodes.complianceDuration,
                deleteIt = statusCodes.deleteIt,
                Description = statusCodes.Description,
                DescriptionLong = statusCodes.DescriptionLong,
                FormID = statusCodes.FormID,
                OnCollectorComplianceReport = statusCodes.OnCollectorComplianceReport,
                SeqNo = statusCodes.SeqNo,
                StatusCode = statusCodes.StatusCode1,
                StatusLevel = statusCodes.StatusLevel
            };
        }
    }
}
