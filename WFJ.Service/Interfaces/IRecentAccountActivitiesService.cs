using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
   public interface IRecentAccountActivitiesService
    {
        List<RecentAccountActivity> GetRecentAccounts(int days);
        List<RecentAccountActivity> GetRecentActivities(int days);

        void AddEdit(int requestID, int UserID, string type);


    }
}
