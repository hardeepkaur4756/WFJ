using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ClientModel
    {
        public int ID { get; set; }
        public string ClientName { get; set; }
        public string ContactName { get; set; }
        public int defaultUserID { get; set; }
        public string Image { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string EMail { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string LevelName { get; set; }
        public int ParentLevelID { get; set; }
        public int ClientNumberx { get; set; }
        public string BusinessSummary { get; set; }
        public string RequestorTitle { get; set; }
        public string RequestDateField { get; set; }
        public byte autoShowFiles { get; set; }
        public byte hasAccountSummary { get; set; }
        public byte hasCollectionSummary { get; set; }
        public byte hasClientReport { get; set; }
        public byte hasExportFiles { get; set; }
        public byte showWFJFees { get; set; }
        public byte Active { get; set; }
    }
}
