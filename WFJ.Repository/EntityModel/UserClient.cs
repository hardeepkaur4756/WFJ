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
    
    public partial class UserClient
    {
        public int ID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> UserID { get; set; }
    
        public virtual Client Client { get; set; }
        public virtual User User { get; set; }
    }
}
