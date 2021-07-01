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

        public List<RecentAccountActivity> GetRecentAccounts(int days, int formId, string type)
        {
            return _recentAccountActivitiesRepo.GetRecentAccounts(days, formId, type);
        }

        public List<RecentAccountActivity> GetRecentActivities(int days, int formId, string type)
        {
            return _recentAccountActivitiesRepo.GetRecentActivities(days, formId, type);
        }


        /// <summary>
        /// Add or Update the RecentAccountActivity table.
        /// </summary>
        /// <param name="recentAccountActivity"></param>
        public void AddEdit(int requestID, int UserID, string type)
        {
            RecentAccountActivity recentAccountActivity = new RecentAccountActivity();
            recentAccountActivity.RequestID = requestID;
            recentAccountActivity.UserID = UserID;
            recentAccountActivity.Type = type;

            recentAccountActivity = _recentAccountActivitiesRepo.GetRecentAccountActivity(recentAccountActivity);
            if(recentAccountActivity!=null)
            {
                recentAccountActivity.CreatedDate = DateTime.Now;
                _recentAccountActivitiesRepo.Update(recentAccountActivity);
            }
           
            else
            {
                _recentAccountActivitiesRepo.Add(recentAccountActivity);
            }

        }


    }

}
