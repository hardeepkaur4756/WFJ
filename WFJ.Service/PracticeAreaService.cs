using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
  public  class PracticeAreaService : IPracticeAreaService
    {
     
        public List<PracticeAreaModel> GetAll()
        {
            IPracticeAreaRepository practiceAreaRepository = new PracticeAreaRepository();
            var practiceAreaModels = practiceAreaRepository.GetAll().ToList();
            return MappingExtensions.MapList<PracticeArea, PracticeAreaModel>(practiceAreaModels);

        }
        public List<SelectListItem> GetAllPracticeArea()
        {
            IPracticeAreaRepository practiceAreaRepo = new PracticeAreaRepository();
            List<SelectListItem> practiceAreaList = new List<SelectListItem>();
            practiceAreaList = practiceAreaRepo.GetAll().Where(x=> !string.IsNullOrEmpty(x.PracticeAreaName)).Select(x => new SelectListItem() { Text = x.PracticeAreaName, Value = x.ID.ToString() }
                ).ToList();
            return practiceAreaList;
        }

    }
}
