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
   public class FormTypeService: IFormTypeService
    {
        public List<FormTypeModel> GetAll()
        {
            IFormTypeRepository formTypeRepository = new FormTypeRepository();
            var formTypes = formTypeRepository.GetAll().ToList();
            return MappingExtensions.MapList<FormType, FormTypeModel>(formTypes);

        }
    }
}
