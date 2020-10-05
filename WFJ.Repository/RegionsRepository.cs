using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class RegionsRepository : GenericRepository<Region>, IRegionsRepository
    {
        private WFJEntities context;
        public RegionsRepository()
        {
            context = new WFJEntities();
        }
    }
}
