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
    public class FormsRepository : GenericRepository<Form>, IFormsRepository
    {
        private readonly WFJEntities _context;
        public FormsRepository(WFJEntities context)
        {
            _context = context;
        }
        public FormsRepository()
        {
            _context = new WFJEntities();
            _context.Configuration.LazyLoadingEnabled = true;
            _context.Configuration.ProxyCreationEnabled = true;
        }

        public IEnumerable<Form> GetFormList(int clientId, int formTypeId, int? userId)//, string searchKeyword)
        {
            //var sSearch = searchKeyword.ToLower();
            IEnumerable<Form> documents;

            //if (clientId != -1 ||  formTypeId != -1)// || searchKeyword != "")
            //{
            documents = _context.Forms.Include(x => x.Client).Include(x => x.FormType).Include(x => x.Requests);
            if (userId != null)
            {
                IUserClientRepository _UserClientRepo = new UserClientRepository();
                var clientList = _UserClientRepo.GetByUserId(userId.Value).Select(x => x.Client.ID);
                documents = documents.Where(x => x.Client != null && clientList.Contains(x.Client.ID));
            }

            if (clientId != -1)
            {
                documents = documents.Where(x => x.ClientID == clientId);
            }
            if (formTypeId != -1)
            {
                documents = documents.Where(x => x.FormTypeID == formTypeId);
            }
            //if (searchKeyword != "")
            //{
            //    documents = documents.Where(x => !string.IsNullOrEmpty(x.DocumentName) ? x.DocumentName.ToLower().Contains(sSearch) : false);
            //}
            //}
            //else
            //{
            //    return null;
            //}

            return documents;
        }

    }
}
