using System.Collections.Generic;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IRequestDocumentRepository
    {
        RequestDocument Save(RequestDocument requestDocument);
        void Delete(int requestDocumentId);
        List<RequestDocument> GetbyRequestId(int requestId);
    }
}
