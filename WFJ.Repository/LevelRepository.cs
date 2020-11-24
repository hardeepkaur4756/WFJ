using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using System.Data.Entity;

namespace WFJ.Repository
{
    public class LevelRepository : GenericRepository<Level>, ILevelRepository
    {
        private WFJEntities context;
        public LevelRepository()
        {
            context = new WFJEntities();
        }

        public IEnumerable<Level> GetByClientID(int ClientId)
        {
            return _context.Levels.Include(x => x.Levels1).Where(x => x.ClientID == ClientId).ToList();
        }
    }
}
