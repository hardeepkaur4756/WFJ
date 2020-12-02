using System.Collections.Generic;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IPaymentService
    {
        List<PaymentViewModel> GetByRequestId(int requestId);
    }
}
