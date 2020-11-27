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

        public IEnumerable<Request> GetRequestsList(int formId, int requestor, int assignedAtorney, int collector, int statusCode, int statusLevel, int levelId, DateTime? beginDate, DateTime? endDate, bool archived, bool activeOnly)
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
                var statusCodes = _statusCodesRepo.GetByFormID(formId).Where(x => x.StatusLevel == 1).Select(x => x.StatusCode1).ToArray();
                requests = requests.Where(x => statusCodes.Contains(x.StatusCode));
            }
            if (levelId != -1)
            {
                requests = requests.Where(x => x.LevelID == levelId);
            }
            if (beginDate != null)
            {
                requests = requests.Where(x => x.RequestDate != null && x.RequestDate >= beginDate);
            }
            if(endDate != null)
            {
                requests = requests.Where(x => x.RequestDate != null && x.RequestDate <= endDate);
            }
            if(activeOnly == true)
            {
                requests = requests.Where(x => x.active == 1);
            }

            return requests;
        }

        public int GetFormActiveRequestsCount(int formId, bool activeOnly = false)
        {
            var statuscodes = _statusCodesRepo.GetActiveStatusCode(formId).ToArray();
            DateTime lastMonth = DateTime.Now.Date.AddDays(-30);
            var requests = _context.Requests.Where(x => x.FormID == formId && statuscodes.Contains(x.StatusCode) && (x.CompletionDate == null || x.CompletionDate >= lastMonth));
            if (activeOnly == true)
                requests = requests.Where(x => x.active == 1);
            return requests.Count();
        }

        public Request GetRequestWithDetail(int requestId)
        {
            return _context.Requests.Include(x => x.User) // collector
                .Include(x => x.User1) // Requestor
                .Include(x => x.Personnel).FirstOrDefault(x => x.ID == requestId);
        }

    }
}
