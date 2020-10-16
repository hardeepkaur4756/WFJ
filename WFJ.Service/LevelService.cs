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
    public class LevelService : ILevelService
    {
        public List<SelectListItem> GetAllRegions()
        {
            ILevelRepository levelRepo = new LevelRepository();
            List<SelectListItem> regionList = new List<SelectListItem>();
            regionList = levelRepo.GetAll().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new SelectListItem() { Text = x.Name, Value = x.ID.ToString() }
                ).ToList();
            return regionList;
        }
    }
}
