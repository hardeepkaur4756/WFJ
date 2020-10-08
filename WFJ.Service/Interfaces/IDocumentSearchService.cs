using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;

namespace WFJ.Service.Interfaces
{
   public interface IDocumentSearchService
    {
        ManageDocumentModel GetDocuments(int clientId, int documentTypeId, int projectTypeId , int practiceAreaId , int categoryId , int formTypeId , string searchKeyword, DataTablesParam param, string sortDir, string sortCol,int pageNo);
        ManageDocumentFilterViewModel GetDocumentById(int id);
        void AddOrUpdate(ManageDocumentFilterViewModel manageDocumentFilterViewModel);
    }
}
