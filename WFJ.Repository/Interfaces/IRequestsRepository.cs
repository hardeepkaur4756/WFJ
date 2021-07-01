using System;
using System.Collections.Generic;

using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IRequestsRepository: IRepository<Request>
    {
        IEnumerable<Request> GetRequestsList(int formId, int requestor, int assignedAtorney, int collector, int statusCode, int statusLevel, int levelId, DateTime? beginDate, DateTime? endDate, bool archived, bool activeOnly);
        int GetFormActiveRequestsCount(int formId, bool activeOnly = false);
        Request GetRequestWithDetail(int requestId);
        IEnumerable<Request> GetRequestByXDays(int days, int formId);
        IEnumerable<Request> GetRequestByStatusName(string statusCodeName, int formId);
        IEnumerable<Request> GetRequestOutOfCompliance(int formId);
        IEnumerable<Request> FollowUpAccounts(int formId);
        IEnumerable<Request> GetByFormId(int formId);
    }
}
