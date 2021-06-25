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
        void Insert(RecentAccountActivity recentAccountActivity);
        void Update(RecentAccountActivity recentAccountActivity);
    }
}
