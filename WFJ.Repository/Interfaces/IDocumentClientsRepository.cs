using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
    public interface IDocumentClientsRepository:IRepository<documentClient>
    {
        List<documentClient> GetByDocumentID(int documentId);
        void DeleteByDocumentId(int documentId);
        List<documentClient> GetByClientID(int clientID);
    }
}
