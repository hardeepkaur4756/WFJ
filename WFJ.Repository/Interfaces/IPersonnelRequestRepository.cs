using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IPersonnelRequestRepository  : IRepository<PersonnelRequest>
    {
        List<PersonnelRequest> GetPersonnelByRequestId(int RequestId);
    }
}
