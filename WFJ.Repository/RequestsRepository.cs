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
            IEnumerable<Request> requests = _context.Requests.Where(x => x.FormID == formId).Include(x=>x.Form).Include(x => x.User).Include(x => x.User1).Include(x => x.Personnel).Include(x=>x.Level);

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
                .Include(x=>x.RequestNotes)
                .Include(x => x.User1) // Requestor
                .Include(x => x.Personnel).FirstOrDefault(x => x.ID == requestId);
        }

        public IEnumerable<Request> GetRequestByXDays(int days, int userId)
        {
            DateTime date = DateTime.Now.AddDays(days);
            if (userId > 0)
            {
                return _context.Requests.Where(x => x.Requestor!=null && x.Requestor.Value == userId && x.RequestDate >= date && x.StatusCode != null && x.FormID != null);

            }
            else
            {
                return _context.Requests.Where(x => x.RequestDate >= date && x.StatusCode != null && x.FormID != null);
            }
        }

        public IEnumerable<Request> GetRequestByStatusName(string statusCodeName)
        {
            var statuscodes = _statusCodesRepo.GetCodesByStatusName(statusCodeName).ToList();
            return _context.Requests.Where(x => statuscodes.Contains(x.StatusCode)).OrderByDescending(x=>x.RequestDate).Take(7).ToList();
        }

        public IEnumerable<Request> GetRequestOutOfCompliance(int userId)
        {
            var statusCodes = _context.StatusCodes.Where(x => x.OnCollectorComplianceReport == 1 && x.StatusLevel == 1).Select(x => x.StatusCode1).Distinct();

            if (userId > 0)
            {
                return _context.Requests.Where(x => x.Requestor != null && x.Requestor.Value == userId && statusCodes.Contains(x.StatusCode) && (x.Form != null &&
            (x.Form.FormTypeID == 1 || x.Form.FormTypeID == 10)) && x.active == 1).OrderByDescending(x => x.RequestDate).Take(7);
            }

            else
            {
                return _context.Requests.Where(x => statusCodes.Contains(x.StatusCode) && (x.Form != null &&
            (x.Form.FormTypeID == 1 || x.Form.FormTypeID == 10)) && x.active == 1).OrderByDescending(x => x.RequestDate).Take(7);
            }
        }

        public IEnumerable<Request> FollowUpAccounts(int clientId, int formId)
        {
                return _context.Requests.Where(x => x.Form != null && x.Form.Client != null && x.Form.Client.ID == clientId && x.Form.ID == formId
                && x.RequestNotes.Any(y => y.FollowupDate == DateTime.UtcNow));
        }
    }
}
