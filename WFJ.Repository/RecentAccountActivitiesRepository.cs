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

        public List<RecentAccountActivity> GetRecentAccounts(int days)
        {
            return _context.RecentAccountActivities.Where(x => x.Type == "Accounts").OrderByDescending(x => x.CreatedDate).Take(days).ToList();
        }
        public List<RecentAccountActivity> GetRecentActivities(int days)
        {
            return _context.RecentAccountActivities.Where(x => x.Type == "Activity").OrderByDescending(x => x.CreatedDate).Take(days).ToList();
        }

    }
}
