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
    
    public partial class Client
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Client()
        {
            this.Users = new HashSet<User>();
        }
    
        public int ID { get; set; }
        public string ClientName { get; set; }
        public string ContactName { get; set; }
        public Nullable<int> defaultUserID { get; set; }
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
        public Nullable<int> ParentLevelID { get; set; }
        public Nullable<int> ClientNumberx { get; set; }
        public string BusinessSummary { get; set; }
        public string RequestorTitle { get; set; }
        public string RequestDateField { get; set; }
        public Nullable<byte> autoShowFiles { get; set; }
        public Nullable<byte> hasAccountSummary { get; set; }
        public Nullable<byte> hasCollectionSummary { get; set; }
        public Nullable<byte> hasClientReport { get; set; }
        public Nullable<byte> hasExportFiles { get; set; }
        public Nullable<byte> showWFJFees { get; set; }
        public Nullable<byte> Active { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> Users { get; set; }
    }
}
