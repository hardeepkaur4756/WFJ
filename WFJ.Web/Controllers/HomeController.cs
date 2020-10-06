using System;
using System.Collections.Generic;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Web.CustomAttribute;
namespace WFJ.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService = new UserService();
        private IClientService _clientService = new ClientService();

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
                profileViewModel.UserId = Convert.ToInt32(Session["UserId"]);
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
                    profileViewModel.UserId = Convert.ToInt32(Session["UserId"]);
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

        public ActionResult ManageUsers()
        {
            ManageUserViewModel manageUserViewModel = new ManageUserViewModel();
            manageUserViewModel.ManagerUserFilterViewModel = new ManagerUserFilterViewModel
            {
                userViewModel = new UserViewModel(),
                Clients = clientService.GetClients(),
                UserType = _userService.GetAllUserTypes(),
                Regions = _userService.GetAllRegions(),
                Forms = _userService.GetAllForms(),
                Active = new List<SelectListItem>
                {
                    new SelectListItem() { Text="Yes",Value="1"},
                    new SelectListItem(){ Text="No",Value="0" }
                },
                DashboardUser = new List<SelectListItem>
                {
                    new SelectListItem() { Text="Yes",Value="1"},
                    new SelectListItem(){ Text="No",Value="0" }
                }
            };
           
            manageUserViewModel.ManagerUserFilterViewModel = managerUserFilterViewModel;

            return View(manageUserViewModel);
        }
      
        [HttpGet]
        public JsonResult GetUsersList(DataTablesParam param, string sortDir, string sortCol, int clientId= -1, int active = -1, string name = "")
        {
            ManageUserModel model = new ManageUserModel();
            int pageNo = 1;
            if (param.iDisplayStart >= param.iDisplayLength)
                pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
                model = _userService.GetUsers(clientId, active, name, param, pageNo, sortDir, sortCol);
            return Json(new
            {
                aaData = model.users,
                param.sEcho,
                iTotalDisplayRecords = model.totalUsersCount,
                iTotalRecords = model.totalUsersCount
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            ManagerUserFilterViewModel managerUserFilterViewModel = new ManagerUserFilterViewModel();
            managerUserFilterViewModel.userViewModel = new UserViewModel();
            managerUserFilterViewModel.UserType = _userService.GetAllUserTypes();
            managerUserFilterViewModel.Regions = _userService.GetAllRegions();
            managerUserFilterViewModel.Forms = _userService.GetAllForms();
            managerUserFilterViewModel.Active = new List<SelectListItem>
                {
                    new SelectListItem() { Text="Yes",Value="1"},
                    new SelectListItem(){ Text="No",Value="0" }
                };
            managerUserFilterViewModel.DashboardUser = new List<SelectListItem>
                {
                    new SelectListItem() { Text="Yes",Value="1"},
                    new SelectListItem(){ Text="No",Value="0" }
                };

            return Json(new { Success = true, Html = this.RenderPartialViewToString("_addEditManageLogin", managerUserFilterViewModel) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddUser(ManagerUserFilterViewModel managerUserFilterViewModel)
        {
            _userService.AddOrUpdate(managerUserFilterViewModel);
            return Json(new { Success = managerUserFilterViewModel.IsSuccess, Message = managerUserFilterViewModel.Message }, JsonRequestBehavior.AllowGet);
            //if (managerUserFilterViewModel.IsSuccess)
            //{
            //    return RedirectToAction("ManageUsers", "Home");
            //}
            //else
            //{
            //    return Json(new { Success = managerUserFilterViewModel.IsSuccess, Message = managerUserFilterViewModel.Message }, JsonRequestBehavior.AllowGet);
            //}
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            ManagerUserFilterViewModel managerUserFilterViewModel = id > 0 ? _userService.GetManageUserById(id) :  new ManagerUserFilterViewModel();
            managerUserFilterViewModel.UserType = _userService.GetAllUserTypes();
            managerUserFilterViewModel.Regions = _userService.GetAllRegions();
            managerUserFilterViewModel.Forms = _userService.GetAllForms();
            managerUserFilterViewModel.Active = new List<SelectListItem>
                {
                    new SelectListItem() { Text="Yes",Value="1"},
                    new SelectListItem(){ Text="No",Value="0" }
                };
            managerUserFilterViewModel.DashboardUser = new List<SelectListItem>
                {
                    new SelectListItem() { Text="Yes",Value="1"},
                    new SelectListItem(){ Text="No",Value="0" }
                };
            
            return Json(new { Success = true, Html = this.RenderPartialViewToString("_addEditManageLogin", managerUserFilterViewModel) }, JsonRequestBehavior.AllowGet);
        }

        //[HttpGet]
        //public ActionResult AddBidding(int id, string viewType)
        //{
        //    AddEditBiddingViewModel vm = id > 0 ? _biddingService.GetByIDVM(id) : new AddEditBiddingViewModel();

        //    if (viewType == "Display")
        //    {
        //        vm.ViewType = "Display";
        //    }
        //    else
        //    {
        //        vm.AppliedOn = id > 0 ? vm.AppliedOn : DateTime.Now;
        //        vm.AppliedUnders = _context.AppliedUnders.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
        //        vm.Developers = _context.Developers.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
        //        vm.Platforms = _context.Platforms.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
        //        vm.ProjectTypes = _context.ProjectTypes.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
        //        vm.Technologies = _context.Technologies.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
        //        //vm.TeadLeads = _context.TeamLeads.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).OrderBy(x => x.Text).ToList();
        //    }
        //    return Json(new { Success = true, Html = this.RenderPartialViewToString("_AddEditBidding", vm) }, JsonRequestBehavior.AllowGet);
        //}
    }
}