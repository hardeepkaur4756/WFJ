using System.Collections.Generic;
using System;
using WFJ.Models;
using WFJ.Service.Model;

namespace WFJ.Service.Interfaces
{
    public interface IPaymentService
    {
        List<PaymentViewModel> GetByRequestId(int requestId);
        PaymentGrid GetPaymentsGrid(UserType user,int requestId, DataTablesParam param, string sortDir, string sortCol, int pageNo,int clientId,DateTime? beginDate,DateTime? endDate, int? ClientUserId);
        ManagePaymentsModel GetEditPayment(int paymentId);
        void DeletePayment(int paymentId);
        void AddUpdatePayment(ManagePaymentsModel model);
        void SendPayments(int requestId, List<int> payments, List<string> users);
        PaymentDetail GetPaymentDetail(int formId, int? requestId);
    }
}
