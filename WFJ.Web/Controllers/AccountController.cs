using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;

namespace WFJ.Web.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            LoginModel loginModel = new LoginModel();
            loginModel.UserCookieCheck = true;
            System.Web.HttpCookie loginUserCookie = HttpContext.Request.Cookies.Get("loginUserCookie");
            if (loginUserCookie != null && loginUserCookie.HasKeys)
            {
                string emailaddress = loginUserCookie["emailaddress"];
                string password = loginUserCookie["password"];
                if (!string.IsNullOrEmpty(emailaddress) && !string.IsNullOrEmpty(password))
                {
                    //loginUserCookie.Values["emailaddress"] = "";
                    //loginUserCookie.Values["password"] = "";
                    loginModel.EMail = emailaddress;
                    loginModel.Password = password;
                    loginModel.UserCookieCheck = true;
                }
            }
            return View(loginModel);
        }

        public ActionResult ForgotPassword(string forgotEmailAddress)
        {
            IUserService userService = new UserService();
            ResultModel resultModel = new ResultModel();
            resultModel = userService.SendForgotPasswordMail(forgotEmailAddress);
            return Json(new { success = resultModel.IsSuccess, message = resultModel.Message }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassword()
        {
            string queryString = "";
            int userId = 0;
            if (!string.IsNullOrEmpty(Request.QueryString.ToString()))
            {

                queryString = Util.Decode(Request.QueryString.ToString());
                string[] temp = queryString.Split('=');
                userId = Convert.ToInt32(temp[1]);
            }
            ViewBag.UserID = userId;
            return View("~/Views/Account/Forgot.cshtml");
        }

        [HttpPost]
        public ActionResult ResetPassword(string newPassword, string newConfirmPassword, int userId)
        {
            IUserService userService = new UserService();
            ResultModel resultModel = new ResultModel();
            resultModel = userService.UpdatePassword(newPassword, newConfirmPassword, userId);
            return Json(new { success = resultModel.IsSuccess, message = resultModel.Message }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string currentPassword, string newPassword, string newConfirmPassword)
        {
            if (Session["UserId"] != null)
            {
                int userId = Convert.ToInt32(Session["UserId"].ToString());
                IUserService userService = new UserService();
                ResultModel resultModel = new ResultModel();
                resultModel = userService.ChangePassword(userId,currentPassword, newPassword, newConfirmPassword);
                return Json(new { success = resultModel.IsSuccess, message = resultModel.Message }, JsonRequestBehavior.AllowGet);
                //return View(userModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }

        [HttpPost]
        public ActionResult Login(LoginModel loginModel)
        {
            ResultModel resultModel = new ResultModel();
            IUserService userService = new UserService();
            resultModel = userService.Login(loginModel);
            if (resultModel.IsPasswordExpire)
            {
                TempData["IsPasswordExpire"] = true;
                return RedirectToAction("ChangePassword", "Account");
            }
            if (resultModel.IsSuccess)
            {
                if (Convert.ToBoolean(loginModel.UserCookieCheck))
                {
                    //Adding username and password in cookies
                    AddUserCookie(loginModel.EMail, loginModel.Password);
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
    }
}