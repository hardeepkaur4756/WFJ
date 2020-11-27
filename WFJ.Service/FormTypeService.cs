using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
   public class FormTypeService: IFormTypeService
    {
        IFormTypeRepository formTypeRepository = new FormTypeRepository();
        public List<FormTypeModel> GetAll()
        {
            var formTypes = formTypeRepository.GetAll().ToList();
            return MappingExtensions.MapList<FormType, FormTypeModel>(formTypes);

        }

        public List<SelectListItem> GetFormTypesDropdown()
        {
            var formTypes = formTypeRepository.GetAll().ToList().Where(x => x.FormType1 != null).Select(x => new SelectListItem { Text = x.FormType1, Value = x.FormTypeID.ToString() }).ToList();
            return DropdownHelpers.PrependALL(formTypes);
        }



    }
}
