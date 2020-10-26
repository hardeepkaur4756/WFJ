using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
   public class ManageUserViewModel:ExceptionModel
    {
        public ManagerUserFilterViewModel ManagerUserFilterViewModel { get; set; }
        public ManageUserDataViewModel ManageUserDataViewModel { get; set; }
    }
    public class ManagerUserFilterViewModel
    {
        //public List<ClientModel> Clients { get; set; }
        public List<SelectListItem> Clients { get; set; }
        public List<SelectListItem> UserType { get; set; }
        public List<SelectListItem> Regions { get; set; }
        public List<SelectListItem> Forms { get; set; }
        public List<SelectListItem> Active { get; set; }
        public List<SelectListItem> DashboardUser { get; set; }
        public UserViewModel userViewModel { get; set; }
        public List<SelectListItem> ManageUsers { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public List<SelectListItem> AccessLevels { get; set; }
        //public string Password { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }

    }
    public class ManageUserDataViewModel
    {
        public List<UserModel> Users { get; set; }
    }
}
