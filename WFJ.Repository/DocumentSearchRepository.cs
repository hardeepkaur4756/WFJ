using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using System.Data.Entity;

namespace WFJ.Repository
{
   public class DocumentSearchRepository:GenericRepository<Document>, IDocumentSearchRepository
    {
        private readonly WFJEntities _context;
        public DocumentSearchRepository(WFJEntities context)
        {
            _context = context;
        }
        public DocumentSearchRepository()
        {
            _context = new WFJEntities();
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public List<Document> GetDocumentList(int clientId, int documentTypeId, int practiceAreaId, int categoryId, int formTypeId, string searchKeyword, int? userId)
        {
            IDocumentClientsRepository _documentClientsRepo = new DocumentClientsRepository(); 
            var sSearch = searchKeyword.ToLower();
            IEnumerable<Document> documents;

            if (clientId != -1 || practiceAreaId !=-1 || documentTypeId!=-1|| categoryId!=-1 || formTypeId!=-1|| searchKeyword != "")
            {
                documents = _context.Documents.Include(s => s.PracticeArea).Include(s => s.Client).ToList();

                if (userId != null)
                {
                    IUserClientRepository _UserClientRepo = new UserClientRepository();
                    var clientList = _UserClientRepo.GetByUserId(userId.Value).Select(x => x.Client.ID);
                    documents = documents.Where(x => x.Client != null && clientList.Contains(x.Client.ID));
                }
                if (clientId != -1)
                {
                    documents = documents.Where(x => _documentClientsRepo.GetByClientID(clientId).Select(y => y.documentID).Contains(x.ID));
                }
                if (documentTypeId != -1)
                {
                    documents = documents.Where(x => x.DocumentTypeID == documentTypeId);
                }
                if (practiceAreaId != -1)
                {
                    documents = documents.Where(x => x.PracticeAreaID == practiceAreaId);
                }
                if (categoryId != -1)
                {
                    documents = documents.Where(x => x.CategoryID == categoryId);
                }
                if (formTypeId != -1)
                {
                    documents = documents.Where(x => x.FormTypeID == Convert.ToString(formTypeId));
                }
                if (searchKeyword != "")
                {
                    documents = documents.Where(x => !string.IsNullOrEmpty(x.DocumentName) ? x.DocumentName.ToLower().Contains(sSearch) : false);

                }
            }
            else
            {
                return null;
            }

            return documents.ToList();
        }
    }
}
