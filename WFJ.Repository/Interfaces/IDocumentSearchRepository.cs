using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Repository.EntityModel;

namespace WFJ.Repository.Interfaces
{
   public interface IDocumentSearchRepository:IRepository<Document>
    {
        List<Document> GetDocumentList(int clientId, int documentTypeId, int practiceAreaId, int categoryId, int formTypeId, string searchKeyword, int? userId);
       
    }
}
