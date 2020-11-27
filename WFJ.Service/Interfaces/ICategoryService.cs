using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
   public interface ICategoryService
    {
        List<CategoryModel> GetAll();
        List<SelectListItem> GetCategoryPracticeAreaDropdown();
    }
}
