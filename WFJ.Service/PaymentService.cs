using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class PaymentService : IPaymentService
    {
        IPaymentsRepository _paymentsRepo = new PaymentsRepository();

        public List<PaymentViewModel> GetByRequestId(int requestId)
        {
            return _paymentsRepo.GetByReqestId(requestId).Select(x => new PaymentViewModel { 
            PaymentDate = x.PaymentDate,
            Amount = x.Amount
            }).ToList();
        }
    }
}
