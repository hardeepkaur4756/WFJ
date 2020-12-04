using System.Collections.Generic;
using System.Linq;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class RequestDocumentRepository : IRequestDocumentRepository
    {
        private WFJEntities _context;

        public RequestDocumentRepository()
        {
            _context = new WFJEntities();
        }

        public void Delete(int requestDocumentId)
        {
            var requestDocument = _context.RequestDocuments.FirstOrDefault(x => x.ID == requestDocumentId);
            _context.RequestDocuments.Remove(requestDocument);
            _context.SaveChanges();
        }

        public List<RequestDocument> GetbyRequestId(int requestId)
        {
            return _context.RequestDocuments.Where(x => x.RequestID == requestId).ToList();
        }

        public RequestDocument Save(RequestDocument requestDocument)
        {
            _context.RequestDocuments.Add(requestDocument);
            _context.SaveChanges();
            return requestDocument;
        }
    }
}
