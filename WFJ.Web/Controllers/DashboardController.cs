using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;
using WFJ.Models;

namespace WFJ.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IErrorLogService _errorLogService = new ErrorLogService();
        public IDashboardService _dashboardService = new DashboardService();

        // GET: Dashboard
        public ActionResult Index()
        {
            int userId = Convert.ToInt32(Session["UserId"].ToString());
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            dashboardViewModel.AdminDashboard = _dashboardService.GetAdminDashboardData();
            dashboardViewModel.UserDashboard = _dashboardService.GetUserDashboardData(userId);
            dashboardViewModel.ClientDashboard = new ClientDashboardViewModel();
            return View(dashboardViewModel);
        }

        [HttpPost]
        public JsonResult GetActiveAccounts()
        {
            ActiveAccountChartViewModel activeAccountChartViewModel = new ActiveAccountChartViewModel();
            activeAccountChartViewModel.Active = 20;
            activeAccountChartViewModel.LocalCounsel = 30;
            activeAccountChartViewModel.InSuit = 25;
            activeAccountChartViewModel.PaymentPlan = 25;
            return Json(activeAccountChartViewModel, JsonRequestBehavior.AllowGet);
        }
    }
}