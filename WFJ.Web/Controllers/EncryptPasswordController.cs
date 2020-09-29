using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Service;
using WFJ.Service.Interfaces;

namespace WFJ.Web.Controllers
{
    public class EncryptPasswordController : Controller
    {
        // GET: EncryptPassword
        public ActionResult Index()
        {
            IUserService userService = new UserService();
            userService.EncryptionPassword();
            return View();
        }
    }
}