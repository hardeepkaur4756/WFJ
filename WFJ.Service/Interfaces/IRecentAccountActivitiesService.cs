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
        List<RecentAccountActivity> GetRecentAccounts(int days, int formId, string type);
        List<RecentAccountActivity> GetRecentActivities(int days, int formId, string type);

        void AddEdit(int requestID, int UserID, string type);


    }
}
