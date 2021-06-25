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
    public class RecentAccountActivitiesRepository : GenericRepository<RecentAccountActivity>, IRecentAccountActivitiesRepository
    {
        public RecentAccountActivity GetRecentAccountActivity(RecentAccountActivity recentAccAct)
        {
            return _context.RecentAccountActivities.Where(x => x.RequestID == recentAccAct.RequestID && x.UserID == recentAccAct.UserID && x.Type == recentAccAct.Type).FirstOrDefault();
        }

    }
}
