using System;
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
    }
}