using System.Collections.Generic;

namespace WFJ.Models
{
    public class SavePlacementViewModel
    {
        public int RequestId { get; set; }
        public string RequestorId { get; set; }
        public string AttorneyId { get; set; }
        public string CollectorId { get; set; }
        public string StatusId { get; set; }
        public string RequestDate { get; set; }
        public string WFJFileCloseDate { get; set; }
        public int FormId { get; set; }
        public List<Fields> FieldValue { get; set; }

    }

    public class Fields
    {
        public int FieldId { get; set; }
        public string FieldValue { get; set; }
        public Address AddressValue { get; set; }
        public int CurrencyId { get; set; }
    }

    public class Address
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string Zipcode { get; set; }
    }
}
