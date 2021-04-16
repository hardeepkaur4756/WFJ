using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class PersonnelRequestRepository : GenericRepository<PersonnelRequest>, IPersonnelRequestRepository
    {
        public List<PersonnelRequest> GetPersonnelByRequestId(int RequestId)
        {
            return _context.PersonnelRequests.Where(x => x.RequestID == RequestId).ToList();
        }
    }
}
