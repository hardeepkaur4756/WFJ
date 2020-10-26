using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class UserViewModel
    {
        public int UserID { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter firstname.")]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter lastname.")]
        public string LastName { get; set; }
        public string Telephone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        [MaxLength(2, ErrorMessage = "The field State must be a string with a maximum length of 2")]
        public string State { get; set; }
        public string PostalCode { get; set; }
        public Nullable<int> UserType { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter email")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Please enter a valid e-mail adress")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please select atleast one client.")]
        public int[] ClientId { get; set; }
        public Nullable<int> AttorneyId { get; set; }
        public List<int> levelID { get; set; }
        public Nullable<byte> IsActive { get; set; }
        public Nullable<byte> IsDashboardUser { get; set; }
        public Nullable<int> ManagerUserId { get; set; }
        //public List<int> RegionId { get; set; }
        //public List<int> FormId { get; set; }
        public int[] RegionId { get; set; }
        public int[] FormId { get; set; }
        public Nullable<int> AccessLevelId { get; set; }
    }
}
