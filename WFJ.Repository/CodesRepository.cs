using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class CodesRepository : GenericRepository<Code>, ICodesRepository
    {
        private WFJEntities context;
        public CodesRepository()
        {
            context = new WFJEntities();
        }
        public List<Code> GetAllByType(string type)
        {
            return context.Codes.Where(x => x.Type.ToLower() == type.ToLower()).ToList();
        }


    }
}
