using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class ManageUserModel
    {
        public int? totalUsersCount { get; set; }
        public List<UserModel> users { get; set; }
    }
}
