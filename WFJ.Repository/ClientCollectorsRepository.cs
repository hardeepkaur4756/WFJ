using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
  public  class ClientCollectorsRepository : GenericRepository<clientCollector>, IClientCollectorsRepository
    {
        public List<int?> GetClientsByUserID(int UserID)
        {
            return (_context.clientCollectors.Where(x => x.collectorID == UserID && x.clientID != null)).Select(x => x.clientID).ToList();
        }
    }
}
