using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class PlacementsModel
    {
        public int ID { get; set; }
        public string FormName { get; set; }
        public Nullable<int> FormTypeID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> AccountBalanceFieldID { get; set; }
        public Nullable<int> TotalPaymentsFieldID { get; set; }
        public Nullable<int> CustomerNameFieldID { get; set; }
        public Nullable<int> WFJFileNbrFieldID { get; set; }
        public Nullable<int> CustomerAccountFieldID { get; set; }
        public Nullable<int> JobNumberFieldID { get; set; }
        public Nullable<int> DefaultRequestorID { get; set; }
        public Nullable<byte> hasCollector { get; set; }
        public Nullable<byte> hasAdmin { get; set; }
        public Nullable<byte> active { get; set; }
        public Nullable<int> ClientNumber { get; set; }

        public string ClientName { get; set; }
        public string FormTypeName { get; set; }
        public int RequestsCount { get; set; }
    }

}
