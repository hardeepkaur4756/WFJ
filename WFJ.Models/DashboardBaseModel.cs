using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class DashboardBaseModel
    {
        public string ClientName { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public int RequestId { get; set; }   
    }
}
