using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class RequestViewModel
    {
        public int ID { get; set; }
        public Nullable<int> FormID { get; set; }
        public Nullable<int> Requestor { get; set; }
        public Nullable<int> AssignedAttorney { get; set; }
        public Nullable<int> AssignedCollectorID { get; set; }
        //public string CollectorStatusCode { get; set; }
        //public Nullable<System.DateTime> CollectorStatusCodeChangeDate { get; set; }
        //public Nullable<int> AssignedAdminStaffID { get; set; }
        //public Nullable<int> LegalAssistantID { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public string RequestDateString { get; set; }
        //public Nullable<int> LevelID { get; set; }
        //public Nullable<int> newLevelID { get; set; }
        //public Nullable<int> oldLevelID { get; set; }
        //public string LocalCounselName { get; set; }
        //public string LocalCounselFileNumber { get; set; }
        //public Nullable<int> reservePercent { get; set; }
        //public Nullable<byte> agingReport { get; set; }
        public Nullable<int> StatusCode { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string CompletionDateString { get; set; }
        public Nullable<byte> active { get; set; } // store 1
        //public Nullable<System.DateTime> LastViewed { get; set; }
        //public Nullable<System.DateTime> LastNoteDate { get; set; }
        //public Nullable<double> TotalPayments { get; set; }
        //public string FirstValue { get; set; }
        //public string Value1 { get; set; }
        //public string Value2 { get; set; }
        //public string Value3 { get; set; }
        //public string Value4 { get; set; }
        //public string Value5 { get; set; }
        //public string Value6 { get; set; }
        //public string Value7 { get; set; }
        //public string Value8 { get; set; }
        //public string Value9 { get; set; }
        //public string Value10 { get; set; }
    }
}
