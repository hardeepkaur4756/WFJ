using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class PersonnelRequestService : IPersonnelRequestService
    {
        public void Add(PersonnelRequestModel personnelRequestModel)
        {
            IPersonnelRequestRepository personnelRequestRepo = new PersonnelRequestRepository();
            var personnelRequest = new PersonnelRequest()
            {
                RequestID = personnelRequestModel.RequestID,
                FirmID = personnelRequestModel.FirmID
            };
            personnelRequestRepo.Add(personnelRequest);
        }
        public bool IsFileAssignedToAssociateCounsel(int requestId)
        {
            bool IsAssigned = false;
            IPersonnelRequestRepository personnelRequestRepo = new PersonnelRequestRepository();
            var result = personnelRequestRepo.GetPersonnelByRequestId(requestId);
            if (result != null)
            {
                IsAssigned = true;
            }
            return IsAssigned;
        }
        public List<PersonnelRequestModel> GetByRequestId(int requestId)
        {
            IPersonnelRequestRepository personnelRequestRepo = new PersonnelRequestRepository();
            var list = personnelRequestRepo.GetPersonnelByRequestId(requestId).ToList();
            return list.Select(x => ConvertEntityToModel(x)).ToList();
        }
        public PersonnelRequestModel ConvertEntityToModel(PersonnelRequest personnelRequest)
        {
            if (personnelRequest == null)
            {
                return new PersonnelRequestModel();
            }
            else
            {
                return new PersonnelRequestModel
                {
                    FirmID = personnelRequest.FirmID,
                    RequestID = personnelRequest.RequestID,
                    localCounselRate = personnelRequest.localCounselRate,
                    localCounselStatus = personnelRequest.localCounselStatus
                };
            }
        }
        public void DeletePersonnelRequest(int requestId)
        {
            IPersonnelRequestRepository personnelRequestRepo = new PersonnelRequestRepository();
            var entity = personnelRequestRepo.GetPersonnelByRequestId(requestId).FirstOrDefault();
            personnelRequestRepo.Remove(entity.ID);
        }
    }
}
