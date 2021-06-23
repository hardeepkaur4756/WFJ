using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ApprovedRecentPayementViewModel: DashboardBaseModel
    {
        public string FormName { get; set; }
        public string PaymentDate { get; set; }
        public double? PaymentAmount { get; set; }
        public double? WFJFees { get; set; }
        public string PaymentType { get; set; }
        public string InvoicDate { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
