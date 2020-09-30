using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult DocumentCenter()
        {
            return View();
        }

        public ActionResult RequestServices()
        {
            return View();
        }

        public ActionResult ManageLogin()
        {
            return View();
        }

        public ActionResult Payment()
        {
            return View();
        }

        public ActionResult ViewRequest()
        {
            return View();
        }

        public ActionResult ViewRequestPayment()
        {
            return View();
        }

        [HttpGet]
        //[AuthorizeActivity((int)Web.Models.Enums.UserType.WFJAdmin)]
        public ActionResult EditProfile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult EditProfile(UserModel userModel)
        {
            if (Session["UserId"] != null)
            {
                userModel.UserID = Convert.ToInt32(Session["UserId"].ToString());
                IUserService userService = new UserService();
                userModel = userService.EditProfile(userModel);
                return View(userModel);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
    }
}