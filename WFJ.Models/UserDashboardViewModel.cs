using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class UserDashboardViewModel
    {
        public List<DashboardBaseModel> RecentlyOpenedAccounts { get; set; }
        public List<ActionRequiredViewModel> ActionRequireds { get; set; }
        public List<ApprovedRecentPayementViewModel> ApprovedPayements { get; set; }
        public List<DashboardBaseModel> FollowUpAccounts { get; set; }
    }
}
