using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class PracticeAreaRepository:GenericRepository<PracticeArea>, IPracticeAreaRepository
    {
        private readonly WFJEntities _context;

        public PracticeAreaRepository()
        {
            _context = new WFJEntities();

        }
    }
}
