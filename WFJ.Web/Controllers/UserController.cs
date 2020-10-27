using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Web.CustomAttribute;
namespace WFJ.Web.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService = new UserService();
        private IClientService _clientService = new ClientService();
        private IErrorLogService _errorLogService = new ErrorLogService();
        private IUserClientService _userClientService = new UserClientService();

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
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/EditProfile", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
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
                        if (_userService.CheckDuplicateByEmailAndUser(profileViewModel.Email, profileViewModel.UserId))
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
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/EditProfile", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
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
                    //Clients = _clientService.GetClients(),
                    Clients = _clientService.GetAllClients(),
                    ManageUsers=new List<SelectListItem>(),
                    UserType = _userService.GetAllUserTypes(),
                    Regions = new List<SelectListItem>(),
                    Forms = new List<SelectListItem>(),
                    AccessLevels=_userService.GetAllAccessLevels(),
                   
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
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/ManageUsers", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new ManageUserViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }

        }

        [HttpGet]
        public JsonResult GetUsersList(DataTablesParam param, string sortDir, string sortCol, int clientId = -1, int active = -1, string name = "")
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
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/GetUsersList", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }

        }
        [HttpGet]
        public ActionResult AddUser()
        {
            try
            {
                int userId = 0;
                ManagerUserFilterViewModel managerUserFilterViewModel = new ManagerUserFilterViewModel();
                managerUserFilterViewModel.userViewModel = new UserViewModel();
                managerUserFilterViewModel.UserType = _userService.GetAllUserTypes();
                managerUserFilterViewModel.Clients = _clientService.GetAllClients();
                managerUserFilterViewModel.AccessLevels = _userService.GetAllAccessLevels();
                managerUserFilterViewModel.Regions = new List<SelectListItem>();
                managerUserFilterViewModel.Forms = new List<SelectListItem>();
                managerUserFilterViewModel.ManageUsers = new List<SelectListItem>();
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
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/AddUser", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }

        }

        [HttpPost]
        public ActionResult AddUser(ManagerUserFilterViewModel managerUserFilterViewModel)
        {
            try
            {
                if ((managerUserFilterViewModel.userViewModel.UserID < 1) && !string.IsNullOrEmpty(managerUserFilterViewModel.userViewModel.Password)) 
                {
                    bool isValidPassword = Util.ValidatePassword(managerUserFilterViewModel.userViewModel.Password);
                    if (!isValidPassword)
                    {
                        ModelState.AddModelError("Password", "The field Password must be a string with minimum of one capital letter, one small letter, one number and one special character is required");
                    }
                }
                if (ModelState.IsValid)
                {
                    _userService.AddOrUpdate(managerUserFilterViewModel);
                    return Json(new { Success = managerUserFilterViewModel.IsSuccess, Message = managerUserFilterViewModel.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    List<string> ErrorMessages = null;
                    var errors = ModelState.Select(x => x.Value.Errors)
                               .Where(y => y.Count > 0)
                               .ToList();
                    if (errors != null)
                    {
                        ErrorMessages = errors.SelectMany(x => x.Select(y => y.ErrorMessage)).ToList();
                    }
                    return Json(new { Success = false, ErrorMessages = ErrorMessages }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/AddUser", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
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

                return Json(new { Success = true, Html = this.RenderPartialViewToString("_addEditManageLogin", managerUserFilterViewModel), ClientId = managerUserFilterViewModel.userViewModel.ClientId, 
                    RegionId = managerUserFilterViewModel.userViewModel.RegionId,
                    FormId = managerUserFilterViewModel.userViewModel.FormId
                }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/EditUser", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }

        [HttpGet]
        public ActionResult BindUserDropdownsByClient(string clientIds,int userId)
        {
            try
            {
                List<SelectListItem> regions = new List<SelectListItem>();
                List<SelectListItem> forms = new List<SelectListItem>();
                List<SelectListItem> manageUsers = new List<SelectListItem>();
                if (!string.IsNullOrEmpty(clientIds))
                {
                    List<int?> values = clientIds.Split(',').Select(x => (int?)Convert.ToInt32(x)).ToList();
                    regions = _userService.GetRegionsByClient(values);
                    forms = _userService.GetFormsByClient(values);
                    manageUsers = _userClientService.GetManageUsersByClient(values, userId);
                    manageUsers.Insert(0, new SelectListItem { Text = "Select", Value = "0" });
                }
                return Json(new { Success = true, regions = regions, forms = forms, manageUsers= manageUsers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "UserController/BindUserDropdownsByClient", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Success = false });
            }
        }
    }
}