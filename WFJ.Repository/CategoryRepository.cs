using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
   public class CategoryRepository:GenericRepository<Category>, ICategoryRepository
    {
        private readonly WFJEntities _context;
        
        public CategoryRepository()
        {
            _context = new WFJEntities();
          
        }
    }
}
