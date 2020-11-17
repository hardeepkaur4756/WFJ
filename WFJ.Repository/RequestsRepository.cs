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
        public RequestsRepository()
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public IEnumerable<Request> GetRequestsList(int formId, int requestor, int assignedAtorney, int collector, int statusCode, DateTime? beginDate, DateTime? endDate)
        {
            IEnumerable<Request> documents;

            documents = _context.Requests.Where(x => x.FormID == formId).Include(x => x.User).Include(x => x.User1).Include(x => x.Personnel);
            //var test = documents.ToList();

            if (requestor != -1)
            {
                documents = documents.Where(x => x.Requestor == requestor);
            }
            if (assignedAtorney != -1)
            {
                documents = documents.Where(x => x.AssignedAttorney == assignedAtorney);
            }
            if (collector != -1)
            {
                documents = documents.Where(x => x.AssignedCollectorID == collector);
            }
            if(statusCode != -1)
            {
                documents = documents.Where(x => x.StatusCode == statusCode);
            }
            if (beginDate != null)
            {
                documents = documents.Where(x => x.RequestDate != null && x.RequestDate >= beginDate);
            }
            if(endDate != null)
            {
                documents = documents.Where(x => x.CompletionDate != null && x.CompletionDate <= endDate);
            }

            return documents;
        }


    }
}
