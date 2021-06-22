using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ActionRequiredViewModel
    {
        public string ClientName { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }
        public string Reason { get; set; }
        public string AttorneyName { get; set; }
        public int? ComplianceDuration { get; set; }
        public string LastNote { get; set; }
        public string LastNoteDate { get; set; }
    }
}

