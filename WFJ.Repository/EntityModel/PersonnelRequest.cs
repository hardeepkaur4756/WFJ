//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WFJ.Repository.EntityModel
{
    using System;
    using System.Collections.Generic;
    
    public partial class PersonnelRequest
    {
        public int ID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> FirmID { get; set; }
        public string AssociateName { get; set; }
        public string Telephone { get; set; }
        public Nullable<int> PersonnelID { get; set; }
        public string fileNumber { get; set; }
        public Nullable<int> localCounselStatus { get; set; }
        public Nullable<double> localCounselRate { get; set; }
    
        public virtual AssociateCounsel AssociateCounsel { get; set; }
        public virtual Request Request { get; set; }
    }
}
