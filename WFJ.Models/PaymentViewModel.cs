using System;

namespace WFJ.Models
{
    public class PaymentViewModel
    {
        public int ID { get; set; }
        public int? RequestID { get; set; }
        public int? UserID { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? remitDate { get; set; }
        public string CheckNumber { get; set; }
        public double? Amount { get; set; }
        public double? WFJFees { get; set; }
        public int? PaymentTypeID { get; set; }
        public int? currencyID { get; set; }
    }
}
