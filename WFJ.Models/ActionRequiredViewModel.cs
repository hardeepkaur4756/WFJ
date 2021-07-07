using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ActionRequiredViewModel : DashboardBaseModel
    {
        public string Reason { get; set; }
        public string AttorneyName { get; set; }
        public int? ComplianceDuration { get; set; }
        public string LastNote { get; set; }
        public string LastNoteDate { get; set; }
        public int RequestId { get; set; }
        public int FormId { get; set; }
    }
}

