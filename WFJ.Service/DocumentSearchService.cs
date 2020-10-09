using System;
using System.Collections.Generic;
using System.IO;
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
        private IDocumentClientsRepository _documentClientsRepo = new DocumentClientsRepository();
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

        public ManageDocumentFilterViewModel GetDocumentById(int id)
        {
            ManageDocumentFilterViewModel manageDocumentFilterViewModel = new ManageDocumentFilterViewModel();
            manageDocumentFilterViewModel.documentViewModel = new DocumentViewModel();
            ICodesService codesService = new CodesService();
            IClientService _clientService = new ClientService();
            manageDocumentFilterViewModel.client = _clientService.GetAllClients();
            Document document = _documentSearchRepository.GetById(id);
            if (document != null)
            {
                manageDocumentFilterViewModel.documentViewModel.Id = document.ID;
                manageDocumentFilterViewModel.documentViewModel.StateCode = document.StateCode;
                manageDocumentFilterViewModel.documentViewModel.DocumentTypeId = document.DocumentTypeID;
                manageDocumentFilterViewModel.documentViewModel.ClientId = _documentClientsRepo.GetByDocumentID(document.ID).Select(x => x.clientID).ToArray();
                manageDocumentFilterViewModel.documentViewModel.PracticeAreaId = document.PracticeAreaID;
                manageDocumentFilterViewModel.documentViewModel.DocumentName = document.DocumentName;
                manageDocumentFilterViewModel.documentViewModel.Description = document.Description;
                manageDocumentFilterViewModel.documentViewModel.FileName = document.FileName;
            }
            foreach (var item in manageDocumentFilterViewModel.client)
            {
                if (manageDocumentFilterViewModel.documentViewModel.ClientId.Any( x => x.ToString() == item.Value))
                {
                    item.Selected = true;
                }
            }
            return manageDocumentFilterViewModel;
        }

        public void AddOrUpdate(ManageDocumentFilterViewModel manageDocumentFilterViewModel)
        {
            try
            {
                if (manageDocumentFilterViewModel.documentFile != null)
                {
                    var postedFile = manageDocumentFilterViewModel.documentFile;
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Document"); 
                        if (!File.Exists(filePath))
                        {
                            Directory.CreateDirectory(filePath);
                        }
                        string saveToPath = Path.Combine(filePath, postedFile.FileName);
                        postedFile.SaveAs(saveToPath);
                    }
                }
                if (manageDocumentFilterViewModel.documentViewModel.Id > 0)
                {
                    Document document = _documentSearchRepository.GetById(manageDocumentFilterViewModel.documentViewModel.Id);
                    document.StateCode = manageDocumentFilterViewModel.documentViewModel.StateCode;
                    document.DocumentTypeID = manageDocumentFilterViewModel.documentViewModel.DocumentTypeId;
                    document.PracticeAreaID = manageDocumentFilterViewModel.documentViewModel.PracticeAreaId;
                    document.DocumentName = manageDocumentFilterViewModel.documentViewModel.DocumentName;
                    document.Description = manageDocumentFilterViewModel.documentViewModel.Description;
                    document.FileName = manageDocumentFilterViewModel.documentFile !=null ?manageDocumentFilterViewModel.documentFile.FileName : document.FileName;
                    _documentSearchRepository.Update(document);
                    if (manageDocumentFilterViewModel.documentViewModel.ClientId != null)
                    {
                        _documentClientsRepo.DeleteByDocumentId(manageDocumentFilterViewModel.documentViewModel.Id);  
                        foreach (var itemId in manageDocumentFilterViewModel.documentViewModel.ClientId)
                        {
                            documentClient dClient = new documentClient()
                            {
                                documentID = manageDocumentFilterViewModel.documentViewModel.Id,
                                clientID = Convert.ToInt32(itemId)
                            };
                            _documentClientsRepo.Add(dClient);
                        }
                    }
                    
                    manageDocumentFilterViewModel.IsSuccess = true;
                    manageDocumentFilterViewModel.Message = "Record Updated Successfully.";
                }
                else
                {
                    Document newDocument = new Document()
                    {
                        StateCode = manageDocumentFilterViewModel.documentViewModel.StateCode,
                        DocumentTypeID = manageDocumentFilterViewModel.documentViewModel.DocumentTypeId,
                        //DocumentTypeID = manageDocumentFilterViewModel.documentViewModel.DocumentTypeId,
                        PracticeAreaID = manageDocumentFilterViewModel.documentViewModel.PracticeAreaId,
                        DocumentName = manageDocumentFilterViewModel.documentViewModel.DocumentName,
                        Description = manageDocumentFilterViewModel.documentViewModel.Description,
                        FileName = manageDocumentFilterViewModel.documentFile != null ? manageDocumentFilterViewModel.documentFile.FileName : null,
                    };
                    _documentSearchRepository.Add(newDocument);
                    if (newDocument.ID > 0 && (manageDocumentFilterViewModel.documentViewModel.ClientId != null))
                    {
                        foreach (var itemId in manageDocumentFilterViewModel.documentViewModel.ClientId)
                        {
                            documentClient dClient = new documentClient()
                            {
                                documentID = newDocument.ID,
                                clientID = Convert.ToInt32(itemId)
                            };
                            _documentClientsRepo.Add(dClient);
                        }

                    }
                    manageDocumentFilterViewModel.IsSuccess = true;
                    manageDocumentFilterViewModel.Message = "Record Inserted Successfully.";

                }
            }
            catch (Exception ex)
            {
                manageDocumentFilterViewModel.IsSuccess = false;
                manageDocumentFilterViewModel.Message = "Sorry, An error occurred!.";
                //throw;
            }

        }
    }
}
