using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using System.Data.Entity;

namespace WFJ.Repository
{
    public class RequestsRepository : GenericRepository<Request>, IRequestsRepository
    {
        IStatusCodesRepository _statusCodesRepo = new StatusCodesRepository();
        public RequestsRepository()
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public IEnumerable<Request> GetRequestsList(int formId, int requestor, int assignedAtorney, int collector, int statusCode, int statusLevel, DateTime? beginDate, DateTime? endDate, bool archived)
        {
            IEnumerable<Request> requests = _context.Requests.Where(x => x.FormID == formId).Include(x => x.User).Include(x => x.User1).Include(x => x.Personnel);

            if(archived == true)
            {
                requests = requests.Where(x => x.CompletionDate != null);
            }
            else // Include completion date is null or last month
            {
                DateTime lastMonth = DateTime.Now.Date.AddDays(-30);
                requests = requests.Where(x => x.CompletionDate == null || x.CompletionDate >= lastMonth);
            }
            if (requestor != -1)
            {
                requests = requests.Where(x => x.Requestor == requestor);
            }
            if (assignedAtorney != -1)
            {
                requests = requests.Where(x => x.AssignedAttorney == assignedAtorney);
            }
            if (collector != -1)
            {
                requests = requests.Where(x => x.AssignedCollectorID == collector);
            }
            if(statusCode != -1)
            {
                requests = requests.Where(x => x.StatusCode == statusCode);
            }
            if(statusLevel != -1)
            {
                var statusCodes = _statusCodesRepo.GetByFormID(formId).Select(x => x.StatusCode1).ToArray();
                requests = requests.Where(x => statusCodes.Contains(x.StatusCode));
                //requests = (from r in requests
                //            join s in statusCodes.Where(x => x.StatusLevel == statusLevel) on r.StatusCode equals s.StatusCode1 into sr
                //            from st in sr.Take(1)
                //            //where st.StatusLevel == statusLevel
                //            select r);

            }
            if (beginDate != null)
            {
                requests = requests.Where(x => x.RequestDate != null && x.RequestDate >= beginDate);
            }
            if(endDate != null)
            {
                requests = requests.Where(x => x.RequestDate != null && x.RequestDate <= endDate);
            }

            return requests;
        }

        public int GetFormActiveRequestsCount(int formId)
        {
            var statuscodes = _statusCodesRepo.GetActiveStatusCode(formId).ToArray();
            DateTime lastMonth = DateTime.Now.Date.AddDays(-30);
            var requestsCount = _context.Requests.Where(x => x.FormID == formId && statuscodes.Contains(x.StatusCode) && (x.CompletionDate == null || x.CompletionDate >= lastMonth)).Count();
            return requestsCount;
        }
    }
}
