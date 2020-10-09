using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Repository
{
    public class DocumentClientsRepository : GenericRepository<documentClient>, IDocumentClientsRepository
    {
        private readonly WFJEntities _context;

        public DocumentClientsRepository()
        {
            _context = new WFJEntities();
        }
        public List<documentClient> GetByDocumentID(int documentId)
        {
            return _context.documentClients.Where(x => x.documentID == documentId).ToList();
        }

        public void DeleteByDocumentId(int documentId)
        {
            _context.documentClients.RemoveRange(_context.documentClients.Where(x => x.documentID == documentId));
            _context.SaveChanges();
        }

        public List<documentClient> GetByClientID(int clientID)
        {
            return _context.documentClients.Where(x => x.clientID == clientID).ToList();
        }
    }
    
}
