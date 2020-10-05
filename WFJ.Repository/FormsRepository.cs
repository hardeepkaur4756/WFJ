using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class FormsRepository : GenericRepository<Form>, IFormsRepository
    {
        private WFJEntities context;
        public FormsRepository()
        {
            context = new WFJEntities();
        }
    }
}
