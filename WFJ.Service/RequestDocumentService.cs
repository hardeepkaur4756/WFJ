using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class RequestDocumentService : IRequestDocumentService
    {
        IRequestDocumentRepository requestDocumentRepo = new RequestDocumentRepository();

        public void Delete(int requestDocumentId)
        {
            requestDocumentRepo.Delete(requestDocumentId);
        }

        public List<RequestDocumentDetail> GetbyRequestId(int requestId)
        {
            var requestDocuments = requestDocumentRepo.GetbyRequestId(requestId);
            return requestDocuments.Select(x => new RequestDocumentDetail
            { 
            DocumentTypeId = Convert.ToInt32(x.DocumentType),
            FileName = x.FileName,
            RequestDocumentId = x.ID,
            RequestId = Convert.ToInt32(x.RequestID),
            PhysicalPathFileName = x.PhysicalPathFileName
            }).ToList();
        }

        public void Save(RequestDocumentDetail requestDocumentViewModel)
        {
            RequestDocument requestDocument = new RequestDocument
            {
                RequestID = requestDocumentViewModel.RequestId,
                DocumentType = requestDocumentViewModel.DocumentTypeId,
                FileName = requestDocumentViewModel.FileName,
                PhysicalPathFileName = requestDocumentViewModel.PhysicalPathFileName
            };
           var request =  requestDocumentRepo.Save(requestDocument);
        }
    }
}
