using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class LoginViewModel:ExceptionModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter username/email address.")]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter password.")]
        public string Password { get; set; }
        public bool UserCookieCheck { get; set; }
    }
}
