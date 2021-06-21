using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ApprovedRecentPayementViewModel
    {
        public string ClientName { get; set; }
        public string CustomerName { get; set; }
        public string FormName { get; set; }
        public string Status { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentAmount { get; set; }
        public string WFJFees { get; set; }
        public string PaymentType { get; set; }
        public string InvoicDate { get; set; }
        public string InvoiceNumber { get; set; }
    }
}
