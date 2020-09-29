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

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult ForgotPassword(string forgotEmailAddress)
        {
            IUserService userService = new UserService();
            ResultModel resultModel = new ResultModel();
            resultModel = userService.SendForgotPasswordMail(forgotEmailAddress);
            return Json(new { success=resultModel.IsSuccess,message = resultModel.Message }, JsonRequestBehavior.AllowGet);
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
            resultModel= userService.UpdatePassword(newPassword, newConfirmPassword, userId);
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
            IUserService userService = new UserService();
            ResultModel resultModel = new ResultModel();
            resultModel = userService.ChangePassword(currentPassword,newPassword, newConfirmPassword);
            return Json(new { success = resultModel.IsSuccess, message = resultModel.Message }, JsonRequestBehavior.AllowGet);
        }
    }
}