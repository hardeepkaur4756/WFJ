using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class RequestsService : IRequestsService
    {
        IRequestsRepository _requestsRepo = new RequestsRepository();
        public RequestViewModel GetByRequestId(int RequestID)
        {
            var request =_requestsRepo.GetById(RequestID);
            RequestViewModel model = new RequestViewModel
            {
                ID = RequestID,
                AssignedAttorney = request.AssignedAttorney,
                AssignedCollectorID = request.AssignedCollectorID,
                FormID = request.FormID,
                RequestDate = request.RequestDate,
                Requestor = request.Requestor,
                StatusCode = request.StatusCode,

            };

            return model;
        }
    }
}
