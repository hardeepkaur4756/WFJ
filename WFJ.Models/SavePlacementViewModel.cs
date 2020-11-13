using System.Collections.Generic;

namespace WFJ.Models
{
    public class SavePlacementViewModel
    {
        public int RequestorId { get; set; }
        public int AttorneyId { get; set; }
        public int CollectorId { get; set; }
        public int StatusId { get; set; }
        public string RequestDate { get; set; }
        public string WFJFileCloseDate { get; set; }
        public List<Fields> FieldValue { get; set; }

    }

    public class Fields
    {
        public int FieldId { get; set; }
        public string FieldValue { get; set; }
    }
}
