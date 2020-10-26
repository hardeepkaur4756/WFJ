using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class AccessLevelsRepository : GenericRepository<AccessLevel>, IAccessLevelsRepository
    {
        private WFJEntities context;
        public AccessLevelsRepository()
        {
            context = new WFJEntities();
        }
    }
}
