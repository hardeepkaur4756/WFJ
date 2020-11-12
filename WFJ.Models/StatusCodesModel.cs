namespace WFJ.Models
{
    public class StatusCodesModel
    {
        public int ID { get; set; }
        public int? StatusCode { get; set; }
        public int? ClientID { get; set; }
        public int? FormID { get; set; }
        public int? StatusLevel { get; set; }
        public int? SeqNo { get; set; }
        public string Description { get; set; }
        public string DescriptionLong { get; set; }
        public byte? deleteIt { get; set; }
        public int? complianceDuration { get; set; }
        public byte? OnCollectorComplianceReport { get; set; }
    }
}
