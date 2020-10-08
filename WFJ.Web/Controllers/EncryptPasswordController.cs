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
    public class EncryptPasswordController : Controller
    {
        private IErrorLogService _errorLogService = new ErrorLogService();
        // GET: EncryptPassword
        public ActionResult Index()
        {
            try
            {
                IUserService userService = new UserService();
                userService.EncryptionPassword();
                return View();
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "EncryptPassword/Index", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(ViewBag.ErrorMessage = "Sorry, An error occurred!" );
                
            }
           
        }
    }
}