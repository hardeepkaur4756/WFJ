using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;
using WFJ.Models;
using WFJ.Repository.EntityModel;
using WFJ.Helper;

namespace WFJ.Web.Controllers
{
    public class DashboardController : Controller
    {
        public IErrorLogService _errorLogService = new ErrorLogService();
        public IDashboardService _dashboardService = new DashboardService();
        private IPaymentService _paymentService = new PaymentService();
        private int UserType = 0;
        private int UserId = 0;
        private int? UserAccess;

        // GET: Dashboard
        public ActionResult Index()
        {
            GetSessionUser(out UserId, out UserType, out UserAccess);
            int userId = Convert.ToInt32(Session["UserId"]);
            int userType = Convert.ToInt32(Session["UserType"]);
            DashboardViewModel dashboardViewModel = new DashboardViewModel();
            var selectedForm = new Form();
            (selectedForm, dashboardViewModel.DashbaordFilter) = _dashboardService.GetDashboardFilters(userType, userId);
            dashboardViewModel.AdminDashboard = _dashboardService.GetAdminDashboardData();
            dashboardViewModel.UserDashboard = _dashboardService.GetUserDashboardData(userId, selectedForm);
           
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
        private void GetSessionUser(out int userId, out int userType, out int? userAccess)
        {
            if (Session["UserId"] != null)
            {
                userId = Convert.ToInt32(Session["UserId"].ToString());
                userType = Convert.ToInt32(Session["UserType"].ToString());
                userAccess = Session["UserAccess"] != null ? Convert.ToInt32(Session["UserAccess"].ToString()) : (int?)null;
            }
            else
            {
                userId = 0;
                userType = 0;
                userAccess = 0;
            }
        }

        [HttpPost]
        public ActionResult UpdatePaymentStatus(int paymentId)
        {
            bool isSuccess = false;
            try
            {
              _paymentService.UpdatePaymentStatus(paymentId);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Dashboard/UpdatePaymentStatus", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }
    }
}