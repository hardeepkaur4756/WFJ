using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class LevelRepository : GenericRepository<Level>, ILevelRepository
    {
        private WFJEntities context;
        public LevelRepository()
        {
            context = new WFJEntities();
        }
        
    }
}
