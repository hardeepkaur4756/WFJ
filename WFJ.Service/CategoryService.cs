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
    public class CategoryService: ICategoryService
    {
        private ICategoryRepository _categoryRepository = new CategoryRepository();

        public List<CategoryModel> GetAll()
        {
            var categoryModels = _categoryRepository.GetAll().ToList();
            return MappingExtensions.MapList<Category, CategoryModel>(categoryModels);
        }

        public List<SelectListItem> GetCategoryPracticeAreaDropdown()
        {
            var categories = _categoryRepository.GetAll().Where(x => x.CategoryName != null).Select(x => new SelectListItem { Value = x.CategoryID.ToString(), Text = x.PracticeArea.PracticeAreaName + " ---> " + x.CategoryName }).ToList();

            return categories;
        }


    }
}
