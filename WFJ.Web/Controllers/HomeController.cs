using System;
using System.Collections.Generic;
using System.Linq;
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

        
    }
}