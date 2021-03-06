﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;

using WFJ.Helper;
using WFJ.Models;
using WFJ.Repository.EntityModel;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;
using WFJ.Web.CustomAttribute;
namespace WFJ.Web.Controllers
{
    public class HomeController : Controller
    {
        private IUserService _userService = new UserService();

        private IErrorLogService _errorLogService = new ErrorLogService();
        private IFormService _formService = new FormService();

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