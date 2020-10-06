using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
