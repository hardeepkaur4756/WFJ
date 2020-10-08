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
        private IErrorLogService _errorLogService = new ErrorLogService();

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
            try
            {
                
                if (Session["UserId"] != null)
                {
                    ProfileViewModel profileViewModel = new ProfileViewModel();
                    profileViewModel.UserId = Convert.ToInt32(Session["UserId"]);
                    profileViewModel = _userService.GetById(profileViewModel.UserId);
                    return View(profileViewModel);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
              
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "HomeController/EditProfile", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new ProfileViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        [HttpPost]
        public ActionResult EditProfile(ProfileViewModel profileViewModel)
        {
            try
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
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "HomeController/EditProfile", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new ProfileViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
           
        }

        public ActionResult ManageUsers()
        {
            try
            {
                
                ManageUserViewModel manageUserViewModel = new ManageUserViewModel();
                manageUserViewModel.ManagerUserFilterViewModel = new ManagerUserFilterViewModel
                {
                    userViewModel = new UserViewModel(),
                    Clients = _clientService.GetClients(),
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
                return View(manageUserViewModel);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "HomeController/ManageUsers", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new ManageUserViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
            
        }
      
        [HttpGet]
        public JsonResult GetUsersList(DataTablesParam param, string sortDir, string sortCol, int clientId= -1, int active = -1, string name = "")
        {
            try
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
                    iTotalRecords = model.totalUsersCount,
                    Success=true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "HomeController/GetUsersList", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
           
        }

        [HttpGet]
        public ActionResult AddUser()
        {
            try
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
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "HomeController/AddUser", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }

        }

        [HttpPost]
        public ActionResult AddUser(ManagerUserFilterViewModel managerUserFilterViewModel)
        {
            try
            {
                _userService.AddOrUpdate(managerUserFilterViewModel);
                return Json(new { Success = managerUserFilterViewModel.IsSuccess, Message = managerUserFilterViewModel.Message }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "HomeController/AddUser", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }

        [HttpGet]
        public ActionResult EditUser(int id)
        {
            try
            {
                ManagerUserFilterViewModel managerUserFilterViewModel = id > 0 ? _userService.GetManageUserById(id) : new ManagerUserFilterViewModel();
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
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "HomeController/EditUser", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }
    }
}