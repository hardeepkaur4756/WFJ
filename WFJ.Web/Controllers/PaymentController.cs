using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;

namespace WFJ.Web.Controllers
{
    [CustomAttribute.AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
    public class PaymentController : Controller
    {
        private int UserType = 0;
        private int UserId = 0;
        private int? UserAccess;

        private IErrorLogService _errorLogService = new ErrorLogService();
        private IPaymentService _paymentService = new PaymentService();
        private ICurrenciesService _currencyService = new CurrenciesService();
        private IPaymentTypeService _paymentTypeService = new PaymentTypesService();
        public JsonResult GetPaymentsList(DataTablesParam param, string sortDir, string sortCol, int requestId)
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                PaymentGrid model = new PaymentGrid();
                int pageNo = 1;
                if (param.iDisplayStart >= param.iDisplayLength)
                    pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;


                model = _paymentService.GetPaymentsGrid(requestId, param, pageNo);


                return Json(new
                {
                    aaData = model.Payments,
                    param.sEcho,
                    iTotalDisplayRecords = model.totalCount,
                    iTotalRecords = model.totalCount,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Payment/GetPaymentsList?requestId=" + requestId, CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }

        public ActionResult AddEditPayment(int? paymentId, int? clientId)
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                IUserService _userService = new UserService();
                var user = _userService.GetById(UserId);
                ViewData["LoginUserName"] = user.FirstName + " " + user.LastName;

                ManagePaymentsModel model = new ManagePaymentsModel
                {
                    PaymentDate = DateTime.Now.ToString("MM/dd/yyyy"),
                    EnteredBy = UserId,
                };
                if (paymentId.HasValue)
                {
                    model = _paymentService.GetEditPayment(paymentId.Value);
                }

                model.Users = _userService.GetUsersByClientId(Convert.ToInt32(clientId));
                model.PaymentTypes = _paymentTypeService.GetPaymentTypeDropdown();
                model.Currencies = _currencyService.GetCurrencyDropdown();
                model.Currency = _currencyService.GetDefaultCurrencyId("USD");
                
                return Json(new { Success = true, Html = this.RenderPartialViewToString("_AddPayment", model) }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "RequestNotes/AddEditNote", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }

        }


        [HttpPost]
        public ActionResult AddPayment(ManagePaymentsModel model)
        {
            bool isSuccess = false;
            string balanceDue = "", totalPayment = "", remainingAmount = "";
            string errorMessage = "";
            if (ModelState.IsValid)
            {
                try
                {
                    GetSessionUser(out UserId, out UserType, out UserAccess);
                    _paymentService.AddUpdatePayment(model);
                  var detail = _paymentService.GetPaymentDetail(model.FormId, model.RequestId);
                    isSuccess = true;
                    if (detail.BalanceDue > 0) {
                        balanceDue = detail.BalanceDueCurrency == "USD" ? "$"+ detail.BalanceDue : detail.BalanceDue + " " + detail.BalanceDueCurrency;
                        totalPayment = detail.TotalPaymentCurrency == "USD" ? "$" + detail.TotalPayment :  detail.TotalPayment + " " + detail.TotalPaymentCurrency;
                        remainingAmount = detail.RemainingAmountCurrency == "USD" ? "$" + detail.RemainingAmount : detail.RemainingAmount + " " + detail.RemainingAmountCurrency;
                    }
                }
                catch (Exception ex)
                {
                    _errorLogService.Add(new ErrorLogModel() { Page = "Payment/AddPayment", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                }
            }
            else
            {
                errorMessage = "Form is not valid. Please add mandatory fields";
            }


            return Json(new { success = isSuccess, errorMessage = errorMessage, balanceDue = balanceDue, totalPayment = totalPayment, remainingAmount  = remainingAmount });
        }

        [HttpPost]
        public ActionResult DeletePayment(int paymentId)
        {
            bool isSuccess = false;
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
               _paymentService.DeletePayment(paymentId);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Payment/DeletePayment", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }

        [HttpPost]
        public ActionResult SendPayments(List<int> payments, List<string> users, int requestId)
        {
            bool isSuccess = false;
            try
            {
                _paymentService.SendPayments(requestId, payments, users);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Payment/SendPayments", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }

        public void GetSessionUser(out int userId, out int userType, out int? userAccess)
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
    }
}