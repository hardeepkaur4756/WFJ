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
    
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.UserClients = new HashSet<UserClient>();
            this.UserLevels = new HashSet<UserLevel>();
        }
    
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Image { get; set; }
        public string ManagerName { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<int> UserAccess { get; set; }
        public Nullable<int> LogonCount { get; set; }
        public string EMail { get; set; }
        public Nullable<int> ManagerUserID { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> AttorneyID { get; set; }
        public Nullable<int> levelID { get; set; }
        public Nullable<System.DateTime> PasswordExpirationDate { get; set; }
        public Nullable<byte> Active { get; set; }
        public Nullable<byte> IsCollector { get; set; }
        public Nullable<byte> IsAdminStaff { get; set; }
        public Nullable<System.DateTime> HireDate { get; set; }
        public Nullable<System.DateTime> Birthdate { get; set; }
        public Nullable<byte> AccessClientExtranet { get; set; }
        public string ProfileText { get; set; }
        public Nullable<byte> showHRSection { get; set; }
        public Nullable<byte> dashboardUser { get; set; }
        public bool IsPasswordHashed { get; set; }
    
        public virtual AccessLevel AccessLevel { get; set; }
        public virtual Client Client { get; set; }
        public virtual Level Level { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserClient> UserClients { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserLevel> UserLevels { get; set; }
    }
}
