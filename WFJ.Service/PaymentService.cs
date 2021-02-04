using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class PaymentService : IPaymentService
    {
        IPaymentsRepository _paymentsRepo = new PaymentsRepository();
        IRequestNotesRepository _notesRepo = new RequestNotesRepository();
        IRequestsService _requestsService = new RequestsService();
        ICurrenciesRepository _currencyRepo = new CurrenciesRepository();
        IPaymentTypesRepository _paymentTypeRepo = new PaymentTypesRepository();

        public List<PaymentViewModel> GetByRequestId(int requestId)
        {
            return _paymentsRepo.GetByReqestId(requestId).Select(x => new PaymentViewModel { 
            PaymentDate = x.PaymentDate,
            Amount = x.Amount
            }).ToList();
        }

        public PaymentGrid GetPaymentsGrid(int requestId, DataTablesParam param, int pageNo)
        {
            PaymentGrid model = new PaymentGrid();
            var requests = _paymentsRepo.GetByReqestId(requestId);
            model.totalCount = requests?.Count();

            if (requests != null)
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
                    Currency = x.currencyID,
                    WFJFees = x.WFJFees,
                    WFJReferenceNumber = x.WFJReferenceNumber,
                    WFJReferenceDate = x.WFJReferenceDate != null ? x.WFJReferenceDate.Value.ToString("MM/dd/yyyy") : null,
                    WFJInvoiceDatePaid = x.WFJInvoiceDatePaid != null ? x.WFJInvoiceDatePaid.Value.ToString("MM/dd/yyyy") : null,
                });
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
                Notes = $"A {paymentType?.PaymentTypeDesc} payment was entered in the amount of {model.PaymentAmount} {currency?.currencyCode}",
                NotesDate = DateTime.Now

            };
            _notesRepo.Add(note);

            //Update Request last note date
            if(model.RequestId.HasValue)
            _requestsService.UpdateRequestLastNoteDate(model.RequestId.Value);
        }
    }
}
