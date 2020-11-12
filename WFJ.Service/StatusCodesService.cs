﻿using System.Collections.Generic;
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

        public List<SelectListItem> GetByFormID(int FormID)
        {
            
            var itemList = statusCodesRepo.GetByFormID(FormID).Where(x => x.Description!=null ).Select(x => new SelectListItem() { Text = x.Description, Value = x.StatusCode1.ToString() }).ToList();
            return itemList;
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