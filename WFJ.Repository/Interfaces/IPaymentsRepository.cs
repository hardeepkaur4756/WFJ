using System.Collections.Generic;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IPaymentsRepository
    {
        List<Payment> GetByReqestId(int requestId);
    }
}
