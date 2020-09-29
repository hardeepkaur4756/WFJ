using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword(string forgotEmailAddress)
        {
            IUserService userService = new UserService();
            userService.SendForgotPasswordMail(forgotEmailAddress);
            return View();
        }

        public ActionResult ResetPassword(string data)
        {
            string queryString = "";
            int userId = 0;
            if (Request.QueryString != null)
            {
                queryString = Util.Decode(Request.QueryString.ToString());
                string[] temp = queryString.Split('=');
                userId = Convert.ToInt32(temp[1]);
            }
            ViewBag.UserID = userId;
            return View("~/Views/Account/Forgot.cshtml");
        }

        public ActionResult ChangePassword(string newPassword, string newConfirmPassword, int userId)
        {
            IUserService userService = new UserService();
            userService.UpdatePassword(newPassword, newConfirmPassword, userId);
            return View();
        }
    }
}