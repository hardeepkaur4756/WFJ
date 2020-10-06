using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;


namespace WFJ.Models
{
    public class UserModel
    {
        
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
        public string Salt { get; set; }
        public string ClientName { get; set; }
        public string LevelName { get; set; }
        public string AccessLevelName { get; set; }
        public string Fullname { get; set; }
        public string ActiveStatus { get; set; }

    }

}
