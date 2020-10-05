using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Web.CustomAttribute;
namespace WFJ.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService = new UserService();

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        public ActionResult Index()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        public ActionResult DocumentCenter()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        public ActionResult RequestServices()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        public ActionResult ManageLogin()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        public ActionResult Payment()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        public ActionResult ViewRequest()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        public ActionResult ViewRequestPayment()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        [HttpGet]
        public ActionResult EditProfile()
        {
            if (Session["UserId"] != null)
            {
                ProfileViewModel profileViewModel = new ProfileViewModel();
                profileViewModel.UserId = Convert.ToInt32(Session["UserId"].ToString());
                profileViewModel= _userService.GetById(profileViewModel.UserId);
                return View(profileViewModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel profileViewModel)
        {
            if (ModelState.IsValid)
            {
                if (Session["UserId"] != null)
                {
                    profileViewModel.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    if (_userService.CheckDuplicateByEmailAndUser(profileViewModel.Email,profileViewModel.UserId))
                    {
                        ModelState.AddModelError("Email", "Email address already exist.");
                        return View(profileViewModel);
                    }
                    profileViewModel = _userService.UpdateProfile(profileViewModel);
                    profileViewModel.Message = "Updated Successfully.";
                    return View(profileViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            else
            {
                return View(profileViewModel);
            }
        }

        public ActionResult ManageUsers(int clientId = -1, int active = -1, string name = "")
        {
            IClientService clientService = new ClientService();

            ManageUserViewModel manageUserViewModel = new ManageUserViewModel();
            ManagerUserFilterViewModel managerUserFilterViewModel = new ManagerUserFilterViewModel
            {
                Clients = clientService.GetClients()
            };

            manageUserViewModel.ManagerUserFilterViewModel = managerUserFilterViewModel;

            return View(manageUserViewModel);
        }
      
        [HttpGet]
        public JsonResult GetUsersList(DataTablesParam param, string sortDir, string sortCol, int clientId = 5, int active = -1, string name = "")
        {

            ManageUserModel model = new ManageUserModel();
            int pageNo = 1;
            int? totalCount = 0;
            IUserService userService = new UserService();
            if (param.iDisplayStart >= param.iDisplayLength)
                pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;

                model = userService.GetUsers(clientId, active, name, param, pageNo, sortDir, sortCol);

            totalCount = model.totalUsersCount;
            return Json(new
            {
                aaData = model.users,
                sEcho = param.sEcho,
                iTotalDisplayRecords = totalCount,
                iTotalRecords = totalCount
            }, JsonRequestBehavior.AllowGet);
        }
    }
}