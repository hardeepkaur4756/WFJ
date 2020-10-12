using System;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Web.CustomAttribute;

namespace WFJ.Web.Controllers
{
    public class AccountController : Controller
    {
        private IErrorLogService _errorLogService = new ErrorLogService();
        private IUserService _userService = new UserService();
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            try
            {
                LoginViewModel loginViewModel = new LoginViewModel();
                loginViewModel.UserCookieCheck = true;
                System.Web.HttpCookie loginUserCookie = HttpContext.Request.Cookies.Get("loginUserCookie");
                if (loginUserCookie != null && loginUserCookie.HasKeys)
                {
                    string emailaddress = loginUserCookie["emailaddress"];
                    string password = loginUserCookie["password"];
                    if (!string.IsNullOrEmpty(emailaddress) && !string.IsNullOrEmpty(password))
                    {
                        loginViewModel.Email = emailaddress;
                        loginViewModel.Password = password;
                        loginViewModel.UserCookieCheck = true;
                    }
                }
                return View(loginViewModel);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "AccountController/Login", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new LoginViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
            
        }

        public ActionResult ForgotPassword(string email)
        {
            try
            {
                ResultModel resultModel = _userService.SendForgotPasswordMail(email);
                return Json(new { success = resultModel.IsSuccess, message = resultModel.Message }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "AccountController/ForgotPassword", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { message = "Sorry, An error occurred!", success = false });            
            }
        }

        public ActionResult ResetPassword()
        {
            try
            {
                string queryString = "";
                int userId = 0;
                if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
                {
                    queryString = Util.Decode(Request.QueryString.ToString());
                    string[] temp = queryString.Split('=');
                    userId = Convert.ToInt32(temp[1]);
                }
                ViewBag.UserId = userId;
                return View("~/Views/Account/Forgot.cshtml");
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "AccountController/ResetPassword", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
           
        }

        [HttpPost]
        public ActionResult ResetPassword(string newPassword, string newConfirmPassword, int userId)
        {
            try
            {
                ResultModel resultModel = _userService.UpdatePassword(newPassword, newConfirmPassword, userId);
                return Json(new { success = resultModel.IsSuccess, message = resultModel.Message }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "AccountController/ResetPassword", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { message = "Sorry, An error occurred!", success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string newConfirmPassword)
        {
            try
            {
                      
                if (Session["UserId"] != null)
                {
                    int userId = Convert.ToInt32(Session["UserId"].ToString());
                    ResultModel resultModel = _userService.ChangePassword(userId, currentPassword, newPassword, newConfirmPassword);
                    return Json(new { success = resultModel.IsSuccess, message = resultModel.Message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return RedirectToAction("Login", "Account");
                }
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "AccountController/ChangePassword", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { message = "Sorry, An error occurred!", success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                ResultModel resultModel = _userService.Login(loginViewModel);
                if (resultModel.IsPasswordExpire)
                {
                    TempData["IsPasswordExpire"] = true;
                    return RedirectToAction("ChangePassword", "Account");
                }
                if (resultModel.IsSuccess)
                {
                    if (Convert.ToBoolean(loginViewModel.UserCookieCheck))
                    {
                        //Adding username and password in cookies
                        AddUserCookie(loginViewModel.Email, loginViewModel.Password);
                    }
                    else
                    {
                        AddUserCookie("", "");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Password", resultModel.Message);
                    return View();
                }
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "AccountController/Login", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new LoginViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }

        private void AddUserCookie(string username, string password)
        {
            // Set cookies
            System.Web.HttpCookie loginUserCookie = new System.Web.HttpCookie("loginUserCookie");
            loginUserCookie["emailaddress"] = username;
            loginUserCookie["password"] = password;
            loginUserCookie.Expires = DateTime.Now.AddMonths(3);
            // Add cookie to response
            Response.Cookies.Add(loginUserCookie);
        }

        public ActionResult Logout()
        {
            try
            {
                Session.RemoveAll(); Session.Clear(); Session.Abandon();
                return RedirectToAction("Login", "Account");
            }
            catch (Exception ex)
            {
                string extraInformation = "Method Name = LogOut, Controller = Account";
                return RedirectToAction("Login", "Account", new { message = "An error occurred, please try again." });
            }
        }
    }
}