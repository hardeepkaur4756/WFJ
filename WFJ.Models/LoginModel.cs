using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class LoginModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter UserName.")]
        public string EMail { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password.")]
        public string Password { get; set; }
        public bool UserCookieCheck { get; set; }
    }
}
