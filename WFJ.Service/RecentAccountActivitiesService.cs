using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WFJ.Service.Interfaces;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

using WFJ.Repository;

namespace WFJ.Service
{
    public class RecentAccountActivitiesService : IRecentAccountActivitiesService
    {
        IRecentAccountActivitiesRepository _recentAccountActivitiesRepo = new RecentAccountActivitiesRepository();

        public void Insert(RecentAccountActivity recentAccountActivity)
        {
            recentAccountActivity.CreatedDate = DateTime.Now;
            recentAccountActivity = _recentAccountActivitiesRepo.Add(recentAccountActivity);
        }

        public void Update(RecentAccountActivity recentAccountActivity)
        {
            recentAccountActivity = _recentAccountActivitiesRepo.GetRecentAccountActivity(recentAccountActivity);
            recentAccountActivity.CreatedDate = DateTime.Now;
            _recentAccountActivitiesRepo.Update(recentAccountActivity);
        }
    }
}
