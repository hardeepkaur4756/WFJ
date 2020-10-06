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
    public class CategoryService: ICategoryService
    {
        private ICategoryRepository _categoryRepository = new CategoryRepository();

        public List<CategoryModel> GetAll()
        {
            ICategoryRepository categoryRepository = new CategoryRepository();
            var categoryModels = categoryRepository.GetAll().ToList();
            return MappingExtensions.MapList<Category, CategoryModel>(categoryModels);

        }
    }
}
