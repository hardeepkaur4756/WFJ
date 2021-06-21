using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class AdminDashboardViewModel
    {
        public List<RecentlyOpenedAccountViewModel> RecentlyOpenedAccounts { get; set; }
        public List<RecentlyOpenedClientViewModel> RecentlyOpenedClients { get; set; }
        public List<ActionRequiredViewModel> ActionRequireds { get; set; }
        public List<FinalDemandViewModel> FinalDemands { get; set; }
        public List<ApprovedRecentPayementViewModel> ApprovedPayements { get; set; }
        public List<ApprovedRecentPayementViewModel> RemitPayements { get; set; }

    }
}
