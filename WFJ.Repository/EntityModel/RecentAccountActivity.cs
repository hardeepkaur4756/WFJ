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
    
    public partial class RecentAccountActivity
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
    
        public virtual Request Request { get; set; }
        public virtual User User { get; set; }
    }
}
