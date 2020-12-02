using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class LevelService : ILevelService
    {
        ILevelRepository levelRepo = new LevelRepository();
        public List<SelectListItem> GetAllRegions()
        {
            List<SelectListItem> regionList = new List<SelectListItem>();
            regionList = levelRepo.GetAll().Where(x => !string.IsNullOrEmpty(x.Name)).Select(x => new SelectListItem() { Text = x.Name, Value = x.ID.ToString() }
                ).ToList();
            return regionList;
        }

        public List<SelectListItem> GetRegionsByClientID(int ClientId)
        {
            List<SelectListItem> regionList = new List<SelectListItem>();
            var list = levelRepo.GetByClientID(ClientId).Where(x => x.ParentID == null);
            GetFlatRegionsFromTable(list, regionList, "","parent");
            return regionList.Where(x => x.Text.Trim() != "").ToList();
        }


        void GetFlatRegionsFromTable(IEnumerable<Level> Levels, List<SelectListItem> OrginialList, string InitialSpace,string parentName)
        {
            foreach (var level in Levels.OrderBy(x => x.SeqNo))
            {
                if (string.IsNullOrEmpty(parentName))
                {
                    InitialSpace = "";
                }
                SelectListItem item = new SelectListItem
                {
                    Text = InitialSpace + level.Name,
                    Value = level.ID.ToString()
                };
                OrginialList.Add(item);
                if (level.Levels1 != null && level.Levels1.Count > 0)
                {
                    GetFlatRegionsFromTable(level.Levels1, OrginialList, InitialSpace + "\xA0\xA0\xA0\xA0",level.Name);
                }
            }

        }

    }
}
