using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class AssociateCounselModel
    {
        public int FirmID { get; set; }
        public string FirmName { get; set; }
        public string Name { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public Nullable<bool> Check { get; set; }
        public string W9Image { get; set; }
        public Nullable<bool> W9 { get; set; }
        public string Country { get; set; }
        public string Telephone1 { get; set; }
        public string Ext { get; set; }
        public string Telephone2 { get; set; }
        public string Fax { get; set; }
        public Nullable<bool> GB { get; set; }
        public Nullable<bool> ALQ { get; set; }
        public Nullable<bool> WH { get; set; }
        public string FederalTaxID { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }
        public Nullable<byte> DoNotUse { get; set; }
        public string Notes { get; set; }
    }
}
