using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WFJ.Helper;
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
        private ICodesRepository _codesRepo = new CodesRepository();
        private IErrorLogService _errorLogService = new ErrorLogService();
        public ManageDocumentModel GetDocuments(int clientId, int documentTypeId, int practiceAreaId, int categoryId, int formTypeId, string searchKeyword, DataTablesParam param, string sortDir, string sortCol,int pageNo, int? userId)
        {
            ManageDocumentModel model = new ManageDocumentModel();
            var documents = _documentSearchRepository.GetDocumentList(clientId,documentTypeId,practiceAreaId,categoryId,formTypeId,searchKeyword, userId);
            model.totalUsersCount = documents?.Count();
            if (documents != null)
            {
                switch (sortCol)
                {
                    case "ClientName":
                        if (sortDir == "asc")
                        {
                            documents = documents.OrderBy(x => x.documentClients.FirstOrDefault()?.Client.ClientName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            documents = documents.OrderByDescending(x => x.documentClients.FirstOrDefault()?.Client.ClientName).ToList();
                        }
                        break;

                    case "State":
                        if (sortDir == "asc")
                        {
                             documents = documents.OrderBy(x => x.Code?.Value).ToList();
                        }

                        if (sortDir == "desc")
                        {
                             documents = documents.OrderByDescending(x => x.Code?.Value).ToList();
                        }
                        break;
                    case "DocumentName":
                        if (sortDir == "asc")
                        {
                            documents = documents.OrderBy(x => x.DocumentName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                             documents = documents.OrderByDescending(x => x.DocumentName).ToList();
                        }
                        break;
                    case "DocumentType":
                        if (sortDir == "asc")
                        {
                            documents = documents.OrderBy(x => x.Code1?.Value).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            documents = documents.OrderByDescending(x => x.Code1?.Value).ToList();
                        }
                        break;

                    case "PracticeAreaName":
                        if (sortDir == "asc")
                        {
                            documents = documents.OrderBy(x => x.PracticeArea?.PracticeAreaName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            documents = documents.OrderByDescending(x => x.PracticeArea?.PracticeAreaName).ToList();
                        }

                        break;
                    default:
                        break;
                }
            }
            string filePath = HttpContext.Current.Request.Url.AbsoluteUri.Replace(HttpContext.Current.Request.Url.PathAndQuery, "") + "/Documents/";

            model.documents = documents == null ? new List<DocumentsModel>() : documents?.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength)?.Select(x => new DocumentsModel
            {
                Id = x.ID,
                DocumentName = x.DocumentName,
                FileName = x.FileName,
                StateCodeId = x.StateCodeID,
                ProjectType = x.ProjectType,
                FormType = x.FormType,
                ClientId = x.ClientID,
                PracticeAreaId = x.PracticeAreaID,
                WFJFileNbr = x.WFJFileNbr,
                Days = x.Days,
                Description = x.Description,
                CategoryId = x.CategoryID,
                EmployeeCategoryId = x.EmployeeCategoryID,
                DocumentTypeId = x.DocumentTypeID,
                ProjectTypeId = x.ProjectTypeID,
                FormTypeId = x.FormTypeID,
                SeqNo = x.SeqNo,
                State = x.Code?.Value,
                DocumentType = x.Code1?.Value,
                ClientName = GetClientName(string.Join(", ", x.documentClients.Where(y => !string.IsNullOrEmpty(y.Client.ClientName)).Select(y => y.Client.ClientName))),
                CurrentUserType = Convert.ToInt32(HttpContext.Current.Session["UserType"]),
                DocumentFullPath = !string.IsNullOrEmpty(x.FileName) ? filePath + x.FileName : "",
                PracticeAreaName=x.PracticeArea?.PracticeAreaName
            }).ToList();
            return model;
        }

        private string GetClientName(string stringName)
        {
            if (!string.IsNullOrEmpty(stringName) && stringName.Length > 50)
            {
                stringName = stringName.Substring(0, 50) + "...";
            }
            return stringName;
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
                manageDocumentFilterViewModel.documentViewModel.StateCodeId = document.StateCodeID;
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
                        string filePath = System.Web.HttpContext.Current.Server.MapPath("../Documents"); 
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
                    document.StateCodeID = manageDocumentFilterViewModel.documentViewModel.StateCodeId;
                    document.DocumentTypeID = manageDocumentFilterViewModel.documentViewModel.DocumentTypeId;
                    document.PracticeAreaID = manageDocumentFilterViewModel.documentViewModel.PracticeAreaId;
                    document.DocumentName = manageDocumentFilterViewModel.documentViewModel.DocumentName;
                    document.Description = manageDocumentFilterViewModel.documentViewModel.Description;
                    document.FileName = manageDocumentFilterViewModel.documentFile !=null ?manageDocumentFilterViewModel.documentFile.FileName : document.FileName;
                    _documentSearchRepository.Update(document);
                    _documentClientsRepo.DeleteByDocumentId(manageDocumentFilterViewModel.documentViewModel.Id);
                    if (manageDocumentFilterViewModel.documentViewModel.ClientId != null)
                    {
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
                        StateCodeID = manageDocumentFilterViewModel.documentViewModel.StateCodeId,
                        DocumentTypeID = manageDocumentFilterViewModel.documentViewModel.DocumentTypeId,
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
                _errorLogService.Add(new ErrorLogModel() { Page = "DocumentSearchService/AddOrUpdate", CreatedBy = Convert.ToInt32(HttpContext.Current.Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

        }
    }
}
