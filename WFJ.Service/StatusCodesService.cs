using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
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

    }
}
