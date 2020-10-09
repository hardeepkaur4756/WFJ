using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ProfileViewModel: ExceptionModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage = "Please enter firstname.")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter lastname.")]
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Fax { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [MaxLength(2,ErrorMessage = "The field State must be a string with a maximum length of 2")]
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Image { get; set; }
        public string ManagerName { get; set; }
        public Nullable<System.DateTime> DateAdded { get; set; }
        public Nullable<int> UserType { get; set; }
        public Nullable<int> UserAccess { get; set; }
        public Nullable<int> LogonCount { get; set; }
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please enter email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid email adress")]
        public string Email { get; set; }
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
        public String Message { get; set; }
    }
}
