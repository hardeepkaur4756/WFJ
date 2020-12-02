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
    public class RequestNotesRepository : GenericRepository<RequestNote>, IRequestNotesRepository
    {
        public RequestNotesRepository()
        {
        }

        public IEnumerable<RequestNote> GetRequestNotes(int requestId, bool includeInternalNotes)
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;

            var requests = _context.RequestNotes.Include(x => x.hiddenRequestNotes).Include(x=>x.User);

            if (requestId != -1)
            {
                requests = requests.Where(x => x.RequestID == requestId);
            }
            if(includeInternalNotes == false)
            {
                requests = requests.Where(x => x.internalNote != 1);
            }

            return requests;
        }

    }
}
