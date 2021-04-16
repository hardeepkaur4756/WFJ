using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Dynamic;
using System.Text;
using System.Web;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;

namespace WFJ.Service
{
    public class PaymentService : IPaymentService
    {
         IPaymentsRepository _paymentsRepo = new PaymentsRepository();
         IRequestNotesRepository _notesRepo = new RequestNotesRepository();
         IRequestsService _requestsService = new RequestsService();
         ICurrenciesRepository _currencyRepo = new CurrenciesRepository();
         IPaymentTypesRepository _paymentTypeRepo = new PaymentTypesRepository();
         IRequestsRepository _requestsRepo = new RequestsRepository();
         IStatusCodesRepository _statusCodesRepo = new StatusCodesRepository();
         IUserRepository _userRepo = new UserRepository();
         IPersonnelsRepository _personalRepo = new PersonnelsRepository();
         IFormFieldsRepository _formfieldRepo = new FormFieldsRepository();
         IFormDataRepository _formDataRepo = new FormDataRepository();

        private IFormService _formService = new FormService();
        public List<PaymentViewModel> GetByRequestId(int requestId)
        {
            return _paymentsRepo.GetByReqestId(requestId).Select(x => new PaymentViewModel
            {
                PaymentDate = x.PaymentDate,
                Amount = x.Amount
            }).ToList();
        }

        public PaymentGrid GetPaymentsGrid(UserType userType, int requestId, DataTablesParam param, string sortDir, string sortCol, int pageNo, int clientId, DateTime? beginDate, DateTime? endDate, int? ClientUserId)
        {
            PaymentGrid model = new PaymentGrid();

            var requests = requestId > 0 ? _paymentsRepo.GetByReqestId(requestId) : _paymentsRepo.GetByClientId(clientId, beginDate, endDate, ClientUserId);

            model.totalCount = requests?.Count();
            if (requests.Count > 0)
            {
                var list1 = requests.OrderByDescending(x => x.PaymentDate).Select(x => new ManagePaymentsModel
                {
                    Id = x.ID,
                    RequestId = x.RequestID,
                    PaymentDate = x.PaymentDate != null ? x.PaymentDate.Value.ToString("MM/dd/yyyy") : null,
                    RemitDate = x.remitDate != null ? x.remitDate.Value.ToString("MM/dd/yyyy") : null,
                    EnteredBy = x.UserID,
                    PaymentTypeId = x.PaymentTypeID,
                    PaymentType = x.PaymentType?.PaymentTypeDesc,
                    CheckNumber = x.CheckNumber,
                    PaymentAmount = x.Amount,
                    PaymentAmountStr = (x.Amount ?? 0) > 0 ? _currencyRepo.GetById(x.currencyID)?.currencyCode == "USD" ? $"${x.Amount}" : $"{x.Amount} {_currencyRepo.GetById(x.currencyID)?.currencyCode}"  : "",
                    Currency = x.currencyID,
                    WFJFees = x.WFJFees,
                    WFJFeesStr = (x.WFJFees ?? 0) > 0 ? _currencyRepo.GetById(x.currencyID)?.currencyCode == "USD" ? $"${x.WFJFees}" : $"{x.WFJFees} {_currencyRepo.GetById(x.currencyID)?.currencyCode}" : "",
                    WFJReferenceNumber = x.WFJReferenceNumber,
                    WFJReferenceDate = x.WFJReferenceDate != null ? x.WFJReferenceDate.Value.ToString("MM/dd/yyyy") : null,
                    WFJInvoiceDatePaid = x.WFJInvoiceDatePaid != null ? x.WFJInvoiceDatePaid.Value.ToString("MM/dd/yyyy") : null,
                    Client = x.User.Client.ClientName,

                    Customer = _formfieldRepo.GetFormFieldsByFormID(x.Request.FormID.Value)
                    .FirstOrDefault(y => y.FieldName.ToLower().Trim() == "customer name")?.ID > 0 ? _formDataRepo.GetByRequestId(x.Request.ID)
                    .FirstOrDefault(z => z.FormFieldID == (_formfieldRepo.GetFormFieldsByFormID(x.Request.FormID.Value)
                    .FirstOrDefault(s => s.FieldName.ToLower().Trim() == "customer name")?.ID)).FieldValue : "",

                    Acct = _formfieldRepo.GetFormFieldsByFormID(x.Request.FormID.Value)
                    .FirstOrDefault(y => y.FieldName.ToLower().Trim() == "customer account #")?.ID > 0 ? _formDataRepo.GetByRequestId(x.Request.ID)
                    .FirstOrDefault(z => z.FormFieldID == (_formfieldRepo.GetFormFieldsByFormID(x.Request.FormID.Value)
                    .FirstOrDefault(s => s.FieldName.ToLower().Trim() == "customer account #")?.ID)).FieldValue : "",

                    Status = _statusCodesRepo.GetByStatusCodeAndFormId((x.Request.StatusCode == null) ? 0 : x.Request.StatusCode.Value, (x.Request.FormID == null) ? 0 : x.Request.FormID.Value).Description,
                    assignedAttorney = x.Request.Personnel.ID == ((x.Request.AssignedAttorney == null) ? 0 : x.Request.AssignedAttorney.Value) ? x.Request.Personnel.FirstName + " " + x.Request.Personnel.LastName : "",
                    Collector = x.Request.User.UserID == ((x.Request.AssignedCollectorID == null) ? 0 : x.Request.AssignedCollectorID.Value) ? x.Request.User.FirstName + " " + x.Request.User.LastName : ""
                });

                switch (sortCol)
                {
                    case "Client":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Client).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Client).ToList();
                        }
                        break;
                    case "Customer":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Customer).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Customer).ToList();
                        }
                        break;
                    case "Acct":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Acct).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Acct).ToList();
                        }
                        break;
                    case "Status":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Status).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Status).ToList();
                        }
                        break;
                    case "assignedAttorney":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.assignedAttorney).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.assignedAttorney).ToList();
                        }
                        break;
                    case "Collector":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Collector).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Collector).ToList();
                        }
                        break;
                    case "PaymentDate":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.PaymentDate).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.PaymentDate).ToList();
                        }
                        break;
                    case "CheckNumber":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.CheckNumber).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.CheckNumber).ToList();
                        }
                        break;
                    case "PaymentAmountStr":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.PaymentAmountStr).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.PaymentAmountStr).ToList();
                        }
                        break;
                    case "PaymentType":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.PaymentType).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.PaymentType).ToList();
                        }
                        break;
                    default:
                        break;
                }

                model.Payments = list1.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
            else
            {
                model.Payments = new List<ManagePaymentsModel>();
            }

            return model;
        }

        public ManagePaymentsModel GetEditPayment(int paymentId)
        {
            var payment = _paymentsRepo.GetById(paymentId);
            ManagePaymentsModel model = new ManagePaymentsModel
            {
                Id =payment.ID,
                RequestId = payment.RequestID,
                PaymentDate = payment.PaymentDate != null ? payment.PaymentDate.Value.ToString("MM/dd/yyyy") : null,
                RemitDate = payment.remitDate != null ? payment.remitDate.Value.ToString("MM/dd/yyyy") : null,
                EnteredBy = payment.UserID,
                PaymentTypeId = payment.PaymentTypeID,
                CheckNumber = payment.CheckNumber,
                PaymentAmount = payment.Amount,
                Currency = payment.currencyID,
                WFJFees = payment.WFJFees,
                WFJReferenceNumber = payment.WFJReferenceNumber,
                WFJInvoiceDatePaid = payment.WFJInvoiceDatePaid != null ? payment.WFJInvoiceDatePaid.Value.ToString("MM/dd/yyyy") : null,
                WFJReferenceDate = payment.WFJReferenceDate != null ? payment.WFJReferenceDate.Value.ToString("MM/dd/yyyy") : null,
            };

            return model;
        }

        public void DeletePayment(int paymentId)
        {
            _paymentsRepo.Remove(paymentId);
        }

        public void AddUpdatePayment(ManagePaymentsModel model)
        {
            var paymentId = 0;
            DateTime? paymentDate = string.IsNullOrEmpty(model.PaymentDate) ? (DateTime?)null : Convert.ToDateTime(model.PaymentDate);
            DateTime? remitDate = string.IsNullOrEmpty(model.RemitDate) ? (DateTime?)null : Convert.ToDateTime(model.RemitDate);
            DateTime? WFJReferenceDate = string.IsNullOrEmpty(model.WFJReferenceDate) ? (DateTime?)null : Convert.ToDateTime(model.WFJReferenceDate);
            DateTime? WFJInvoiceDatePaid = string.IsNullOrEmpty(model.WFJInvoiceDatePaid) ? (DateTime?)null : Convert.ToDateTime(model.WFJInvoiceDatePaid);

            if (model.Id == 0)
            {

                Payment payment = new Payment
                {
                    RequestID = model.RequestId,
                    UserID = model.EnteredBy,
                    PaymentDate = paymentDate,
                    remitDate = remitDate,
                    CheckNumber = model.CheckNumber,
                    Amount = model.PaymentAmount,
                    WFJFees = model.WFJFees,
                    PaymentTypeID = model.PaymentTypeId,
                    currencyID = model.Currency,
                    WFJReferenceNumber = model.WFJReferenceNumber,
                    WFJReferenceDate= WFJReferenceDate,
                    WFJInvoiceDatePaid = WFJInvoiceDatePaid
                };
                _paymentsRepo.Add(payment);
                paymentId = payment.ID;
            }
            else
            {
                var payment = _paymentsRepo.GetById(model.Id);
                payment.RequestID = model.RequestId;
                payment.UserID = model.EnteredBy;
                payment.PaymentDate = paymentDate;
                payment.remitDate = remitDate;
                payment.CheckNumber = model.CheckNumber;
                payment.Amount = model.PaymentAmount;
                payment.WFJFees = model.WFJFees;
                payment.PaymentTypeID = model.PaymentTypeId;
                payment.currencyID = model.Currency;
                payment.WFJReferenceNumber = model.WFJReferenceNumber;
                payment.WFJReferenceDate = WFJReferenceDate;
                payment.WFJInvoiceDatePaid = WFJInvoiceDatePaid;
                _paymentsRepo.Update(payment);
            }

            var currency = _currencyRepo.GetById(model.Currency);
            var paymentType = _paymentTypeRepo.GetById(model.PaymentTypeId);

            //Add payment notes

            RequestNote note = new RequestNote
            {
                paymentDate = paymentDate,
                UserID = model.EnteredBy,
                RequestID = model.RequestId,
                Notes = currency?.currencyCode == "USD" ? $"A {paymentType?.PaymentTypeDesc} payment was entered in the amount of ${model.PaymentAmount}" : $"A {paymentType?.PaymentTypeDesc} payment was entered in the amount of {model.PaymentAmount} {currency?.currencyCode}",
                NotesDate = DateTime.Now

            };
            _notesRepo.Add(note);

            //Update Request last note date
            if(model.RequestId.HasValue)
            {
                _requestsService.UpdateRequestLastNoteDate(model.RequestId.Value);
            }

            // send a note at add payment time.
            try
            {
                var request = _requestsRepo.GetRequestWithDetail(model.RequestId.Value);
                var attorneyId = request.AssignedAttorney;
                if (attorneyId == null)
                {
                    attorneyId = Convert.ToInt32(ConfigurationManager.AppSettings["DefaultAttorneyIDForAddNote"]);
                }

                var attorneyUser = _personalRepo.GetById(attorneyId);

                List<int> singlePayment = new List<int>
                {
                    paymentId
                };
                List<string> singleEmail = new List<string>
                {
                    attorneyUser.EMail
                };

                SendPayments(request.ID, singlePayment, singleEmail);
            }
            catch (Exception)
            {

            }
        }

        public void SendPayments(int requestId, List<int> payments, List<string> users)
        {
            var request = _requestsRepo.GetRequestWithDetail(requestId);
            string status = "";
            if (request.StatusCode != null)
            {
                var statuscode = _statusCodesRepo.GetByStatusCodeAndFormId(request.StatusCode.Value, request.FormID.Value);
                status = statuscode.Description;
            }
            string baseUrl = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Payment/SendPayments", "");
            string queryString = baseUrl + "/Placements/AddPlacement?value=" + Util.Encode(request.FormID + "|" + request.ID);
            string subject = "WFJ Payments";
            string dirpath = HttpContext.Current.Server.MapPath("/EmailTemplate");
            string xlsTemplatePath = dirpath + "/PaymentNotes.html";
            string emailTemplate = File.ReadAllText(xlsTemplatePath);

            StringBuilder sb1 = new StringBuilder();
            sb1.Append(emailTemplate);
            sb1.Replace("[RequestUrl]", queryString);
            sb1.Replace("[baseurl]", baseUrl);
            sb1.Replace("[Status]", status);
            sb1.Replace("[Requestor]", request.User1 != null ? request.User1.FirstName + " " + request.User1.LastName : "");
            //sb1.Replace("[Collector]", request.User != null ? request.User.FirstName + " " + request.User.LastName : "" );
            sb1.Replace("[Attorney]", request.Personnel != null ? request.Personnel.FirstName + " " + request.Personnel.LastName : "");

            // Need to implement
            sb1.Replace("[CustomerName]", "");
            sb1.Replace("[CustomerAccount]", "");
            sb1.Replace("[CollectMaxNo]", "");
            sb1.Replace("[WFJFileNo]", "");




            string xlsTemplatePath2 = dirpath + "/PaymentNotesList.html";
            string notesList = "";

            foreach (var id in payments)
            {
                var payment = _paymentsRepo.GetById(id);
                var currency = _currencyRepo.GetById(payment.currencyID);
                var paymentType = _paymentTypeRepo.GetById(payment.PaymentTypeID);
                string note = currency?.currencyCode == "USD" ? $"A {paymentType?.PaymentTypeDesc} payment was entered in the amount of ${payment.Amount}"
                    : $"A {paymentType?.PaymentTypeDesc} payment was entered in the amount of {payment.Amount} {currency?.currencyCode}";
                string noteHtml = File.ReadAllText(xlsTemplatePath2);
                noteHtml = noteHtml.Replace("[NoteDate]", payment.PaymentDate != null ? payment.PaymentDate.Value.ToString("MM/dd/yyyy") : "")
                                   .Replace("[Author]", "")
                                    .Replace("[Notes]", note);
                notesList = notesList + noteHtml;
            }

            sb1.Replace("[NotesList]", notesList);
            string noteEmail = sb1.ToString();

            foreach (var email in users)
            {
                if (Util.ValidateEmail(email))
                {
                    EmailHelper.SendMail(email, subject, noteEmail);
                }
            }
        }

        public PaymentDetail GetPaymentDetail(int formId, int? requestId) {
            PaymentDetail model = new PaymentDetail();
            var form = _formService.GetFormById(Convert.ToInt32(formId));
            int accountBalanceFieldId = Convert.ToInt32(form.AccountBalanceFieldID);
            decimal balanceDue = 0;
            string balanceDueCurrency = "";
            if (accountBalanceFieldId > 0)
            {
                var formField = _formService.GetFormFieldsByForm(Convert.ToInt32(formId), requestId)?.FirstOrDefault(x => x.ID == accountBalanceFieldId);
                balanceDue = Convert.ToDecimal(formField.FormData?.FieldValue);
                balanceDueCurrency = (formField.FormData.currencyID ?? 0) > 0 && balanceDue > 0 ? _currencyRepo.GetById((int)formField.FormData.currencyID)?.currencyCode : balanceDue > 0 ? "USD" : "";
            }

            if (balanceDue > 0 && requestId.HasValue)
            {
                model.BalanceDue = balanceDue;
                model.BalanceDueCurrency = balanceDueCurrency;
                model.TotalPaymentCurrency = balanceDueCurrency;
                model.RemainingAmountCurrency = balanceDueCurrency;

                IPaymentService _paymentService = new PaymentService();
                var payments = _paymentService.GetByRequestId(requestId.Value);
                double? totalPayment = 0;
                if (payments.Any())
                {
                    totalPayment = payments.Sum(x => x.Amount);
                }
                model.TotalPayment = Convert.ToDecimal(totalPayment);
                model.RemainingAmount = balanceDue - Convert.ToDecimal(totalPayment);
            }
            return model;
        }
    }
}
