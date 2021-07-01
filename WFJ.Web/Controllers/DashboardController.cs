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
        private IRequestNotesService _requestNotes = new RequestNotesService();
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
            int firstFormId = int.Parse(dashboardViewModel.DashbaordFilter.FirstOrDefault().Value);
            dashboardViewModel.AdminDashboard = _dashboardService.GetAdminDashboardData(firstFormId);
            dashboardViewModel.UserDashboard = _dashboardService.GetUserDashboardData(firstFormId);
            dashboardViewModel.ClientDashboard = _dashboardService.GetClientDashboardData(firstFormId);
            return View(dashboardViewModel);
        }

        [HttpPost]
        public JsonResult GetActiveAccounts(int formId)
        {
            var chartBaseModel = _dashboardService.GetActiveStatusPieChartData(formId);
            return Json(chartBaseModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPlacementsData(int formId)
        {
            var chartBaseModel = _dashboardService.GetPlacementsLineChartData(formId);
            return Json(chartBaseModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDollarsPlacedData(int formId)
        {
            var chartBaseModel = _dashboardService.GetDollarsPlacedLineChartData(formId);
            return Json(chartBaseModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetPlacementCollectedData(int formId)
        {
            var chartBaseModel = _dashboardService.GetPlacementCollectedLineChartData(formId);
            return Json(chartBaseModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetDollarsCollectedData(int formId)
        {
            var chartBaseModel = _dashboardService.GetPlacementCollectedLineChartData(formId);
            return Json(chartBaseModel, JsonRequestBehavior.AllowGet);
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


        [HttpPost]
        public ActionResult UpdateAlreadySeenStatus(int requestId)
        {
            bool isSuccess = false;
            try
            {
                _requestNotes.UpdateAlreadySeenStatus(requestId);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Dashboard/UpdateAlreadySeenStatus", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }
    }
}