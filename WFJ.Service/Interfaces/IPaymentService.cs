using System.Collections.Generic;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IPaymentService
    {
        List<PaymentViewModel> GetByRequestId(int requestId);
        PaymentGrid GetPaymentsGrid(int requestId, DataTablesParam param, int pageNo);
        ManagePaymentsModel GetEditPayment(int paymentId);
        void DeletePayment(int paymentId);
        void AddUpdatePayment(ManagePaymentsModel model);
    }
}
