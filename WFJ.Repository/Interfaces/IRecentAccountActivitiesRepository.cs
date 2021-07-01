using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IRecentAccountActivitiesRepository : IRepository<RecentAccountActivity>
    {
        RecentAccountActivity GetRecentAccountActivity(RecentAccountActivity recentAccountActivity);
        List<RecentAccountActivity> GetRecentAccounts(int days, int formId, string type);
        List<RecentAccountActivity> GetRecentActivities(int days, int formId, string type);
    }
}
