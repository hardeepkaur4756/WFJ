using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;
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
            itemList = codesRepo.GetAllByType(type).Select(x => new SelectListItem() { Text = x.Value, Value = x.ID.ToString() }).Prepend(new SelectListItem { Text = "Select", Value = "-1" }).ToList();
            return itemList;
        }

        public List<SelectListItem> GetAllStateByType(string type)
        {
            ICodesRepository codesRepo = new CodesRepository();
            List<SelectListItem> itemList = new List<SelectListItem>();
            itemList = codesRepo.GetAllByType(type).Select(x => new SelectListItem() { Text = x.Value, Value = x.ID.ToString() }).Prepend(new SelectListItem { Text = "Select", Value = "-1" }).ToList();
            return itemList;
        }
        public CodeModel GetById(int id)
        {
            ICodesRepository codesRepo = new CodesRepository();
            var codeModel = new CodeModel();
            var entity = codesRepo.GetById(id);
            if (entity == null)
            {
                return codeModel;
            }
            else
            {
                return new CodeModel
                {
                    ID = entity.ID,
                    Code1 = entity.Code1,
                    Value = entity.Value,
                    Type = entity.Type
                };
            }
        }
    }
}
