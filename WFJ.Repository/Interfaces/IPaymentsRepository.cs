using System;
using System.Collections.Generic;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IPaymentsRepository : IRepository<Payment>
    {
        List<Payment> GetByReqestId(int requestId);
        List<Payment> GetByClientId(int clientId, DateTime? beginDate, DateTime? endDate,int? ClientUserId);
        IEnumerable<Payment> GetPaymentByApprovedAndXDays(int approved);
    }
}
