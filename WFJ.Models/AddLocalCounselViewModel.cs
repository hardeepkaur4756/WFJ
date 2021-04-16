using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class AddLocalCounselViewModel : ExceptionModel
    {
        public int FirmId { get; set; }
        public string FirmName { get; set; }
        public string AttorneyName { get; set; }
        public string Address { get; set; }
        public string Suite { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string Country { get; set; }
        public string PhoneNumber { get; set; }
        public string DirectLine { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string FederalTaxId { get; set; }
        public bool Check { get; set; }
        public bool W9 { get; set; }
        public bool ALQ { get; set; }
        public bool GeneralBar { get; set; }
        public bool WrightHolmess { get; set; }
        public bool DoNotUse { get; set; }
        public string Notes { get; set; }
        public List<FileInformation> fileInformation {get;set;}
    }
    public class FileInformation
    {
        public string Client { get; set; }
        public string CustomerName { get; set; }
        public string WfjFile { get; set; }
        public string AttorneyName { get; set; }
        public string LienCollection { get; set; }
        public string Status { get; set; }
        public string Path { get; set; }
    }
}
