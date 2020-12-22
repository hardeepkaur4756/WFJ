using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class PersonnelsRepository : GenericRepository<Personnel>, IPersonnelsRepository
    {
        public PersonnelsRepository()
        {
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public Personnel GetEmailByPersonelRequestId(int requestId)
        {
            var personnelRequest = _context.PersonnelRequests.FirstOrDefault(x => x.RequestID == requestId);
            if(personnelRequest?.PersonnelID != null)
            {
                return _context.Personnels.FirstOrDefault(x => x.ID == personnelRequest.PersonnelID);
            }
            else
            {
                return null;
            }
        }
    }
}
