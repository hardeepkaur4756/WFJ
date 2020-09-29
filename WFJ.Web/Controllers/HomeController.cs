using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Service;
using WFJ.Service.Interfaces;

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

        public ActionResult EditProfile()
        {
            return View();
        }
    }
}