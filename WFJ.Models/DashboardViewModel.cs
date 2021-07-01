using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class DashboardViewModel
    {
        public ClientDashboardViewModel ClientDashboard { get; set; }
        public UserDashboardViewModel UserDashboard { get; set; }
        public AdminDashboardViewModel AdminDashboard { get; set; }
        public List<SelectListItem> DashbaordFilter { get; set; }
        public int FormId { get; set; }
    }
}
