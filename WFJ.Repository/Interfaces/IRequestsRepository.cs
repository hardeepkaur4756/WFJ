using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IRequestsRepository: IRepository<Request>
    {
        IEnumerable<Request> GetRequestsList(int formId, int requestor, int assignedAtorney, int collector, int statusCode, int statusLevel, DateTime? beginDate, DateTime? endDate, bool archived);
        int GetFormActiveRequestsCount(int formId);
    }
}
