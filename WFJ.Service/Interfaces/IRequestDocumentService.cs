using System.Collections.Generic;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IRequestDocumentService
    {
        void Save(RequestDocumentDetail requestDocumentViewModel);
        void Delete(int requestDocumentId);
        List<RequestDocumentDetail> GetbyRequestId(int requestId);
    }
}
