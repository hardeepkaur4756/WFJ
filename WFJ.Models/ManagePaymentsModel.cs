using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class PaymentGrid
    {
        public int? totalCount { get; set; }
        public List<ManagePaymentsModel> Payments { get; set; }
    }
    public class ManagePaymentsModel
    {
        public int Id { get; set; }
        [Required]
        public int? RequestId { get; set; }
        [Required]
        public string PaymentDate { get; set; }
        public string RemitDate { get; set; }
        [Required]
        public int? EnteredBy { get; set; }
        [Required]
        public int? PaymentTypeId { get; set; }
        public string PaymentType { get; set; }
        public string CheckNumber { get; set; }
        [Required]
        public double? PaymentAmount { get; set; }
        [Required]
        public int? Currency { get; set; }
        public string CurrencyCode { get; set; }
        public double? WFJFees { get; set; }
        public string WFJReferenceNumber { get; set; }
        public string WFJReferenceDate { get; set; }
        public string WFJInvoiceDatePaid { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<SelectListItem> PaymentTypes { get; set; }
        public List<SelectListItem> Currencies { get; set; }
        public string PaymentAmountStr { get; set; }
        public string WFJFeesStr { get; set; }
        public int FormId { get; set; }
    }
}
