using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IPersonnelRequestService 
    {
        void Add(PersonnelRequestModel personnelRequestModel);
        bool IsFileAssignedToAssociateCounsel(int requestId);
        List<PersonnelRequestModel> GetByRequestId(int requestId);
        void DeletePersonnelRequest(int requestId);
    }
}
