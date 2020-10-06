using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class DocumentSearchService: IDocumentSearchService
    {
        private IDocumentSearchRepository _documentSearchRepository = new DocumentSearchRepository();
        public ManageDocumentModel GetDocuments(int clientId, int documentTypeId, int projectTypeId, int practiceAreaId, int categoryId, int formTypeId, string searchKeyword, DataTablesParam param, string sortDir, string sortCol,int pageNo)
        {
            ManageDocumentModel model = new ManageDocumentModel();
            var documents = _documentSearchRepository.GetDocumentList(clientId,documentTypeId, projectTypeId,practiceAreaId,categoryId,formTypeId,searchKeyword);
            model.totalUsersCount = documents?.Count();
            switch (sortCol)
            {
                case "ClientName":
                    if (sortDir == "asc")
                    {
                        documents = documents.Where(x => x.Client != null)?.OrderBy(x => x.Client.ClientName).ToList();
                    }
                    if (sortDir == "desc")
                    {
                        documents = documents.Where(x => x.Client != null)?.OrderByDescending(x => x.Client.ClientName).ToList();
                    }
                    break;

                case "StateCodeID":
                    if (sortDir == "asc")
                    {
                        if (documents != null)
                        {
                            documents = documents.OrderBy(x => x.StateCodeID).ToList();
                        }
                    }

                    if (sortDir == "desc")
                    {
                        if (documents != null)
                        {
                            documents = documents.OrderByDescending(x => x.StateCodeID).ToList();
                        }

                    }
                    break;
                case "DocumentName":
                    if (sortDir == "asc")
                    {
                        if (documents != null) { 
                            documents = documents.OrderBy(x => x.DocumentName).ToList();
                        }
                    }
                    if (sortDir == "desc")
                    {
                        if (documents != null)
                        {
                            documents = documents.OrderByDescending(x => x.DocumentName).ToList();
                        }
                    }
                    break;
                case "DocumentTypeID":
                    if (sortDir == "asc")
                    {
                        if (documents != null)
                        {
                            documents = documents.OrderBy(x => x.DocumentTypeID).ToList();
                        }
                    }
                    if (sortDir == "desc")
                    {
                        if (documents != null)
                        {
                            documents = documents.OrderByDescending(x => x.DocumentTypeID).ToList();
                        }
                    }
                    break;

                case "PracticeAreaName":
                    if (sortDir == "asc")
                    {
                        documents = documents.Where(x => x.PracticeArea != null)?.OrderBy(x => x.PracticeArea.PracticeAreaName).ToList();
                    }
                    if (sortDir == "desc")
                    {
                        documents = documents.Where(x => x.PracticeArea != null)?.OrderByDescending(x => x.PracticeArea.PracticeAreaName).ToList();
                    }

                    break;
                    default:
                    break;
            }

            model.documents = MappingExtensions.MapList<Document, DocumentsModel>(documents?.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList());
           
            return model;
        }
    }
}
