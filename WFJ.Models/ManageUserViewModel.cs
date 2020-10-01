using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class ManageUserViewModel
    {
        public ManagerUserFilterViewModel ManagerUserFilterViewModel { get; set; }
        public ManageUserDataViewModel ManageUserDataViewModel { get; set; }
    }
    public class ManagerUserFilterViewModel
    {
        public List<ClientModel> Clients { get; set; }
    }
    public class ManageUserDataViewModel
    {
        public List<UserModel> Users { get; set; }
    }
}
