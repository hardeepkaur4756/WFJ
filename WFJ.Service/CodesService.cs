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
    public class CodesService : ICodesService
    {
        public List<SelectListItem> GetAllByType(string type)
        {
            ICodesRepository codesRepo = new CodesRepository();
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList = codesRepo.GetAllByType(type).Select(x => new SelectListItem() { Text = x.Value, Value = x.ID.ToString() }).ToList();
            return itemList;
        }

        public List<SelectListItem> GetAllStateByType(string type)
        {
            ICodesRepository codesRepo = new CodesRepository();
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList = codesRepo.GetAllByType(type).Select(x => new SelectListItem() { Text = x.Value, Value = x.ID.ToString() }).ToList();
            return itemList;
        }
    }
}
