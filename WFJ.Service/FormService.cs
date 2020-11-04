using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Service.Interfaces;

namespace WFJ.Service
{
    public class FormService : IFormService
    {
        public List<SelectListItem> GetAllForms()
        {
            IFormsRepository formsRepo = new FormsRepository();
            List<SelectListItem> fornList = new List<SelectListItem>();
            fornList = formsRepo.GetAll().Where(x => !string.IsNullOrEmpty(x.FormName)).Select(x => new SelectListItem() { Text = x.FormName, Value = x.ID.ToString() }
                ).ToList();
            return fornList;
        }

        private IFormsRepository _formSearchRepository = new FormsRepository();
        //private IDocumentClientsRepository _documentClientsRepo = new DocumentClientsRepository();
        //private ICodesRepository _codesRepo = new CodesRepository();
        private IErrorLogService _errorLogService = new ErrorLogService();
        public ManagePlacementsModel GetPlacements(int clientId, int formTypeId, string searchKeyword, DataTablesParam param, string sortDir, string sortCol, int pageNo, int? userId)
        {
            ManagePlacementsModel model = new ManagePlacementsModel();
            var documents = _formSearchRepository.GetFormList(clientId, formTypeId, userId);


            model.totalPlacementsCount = documents?.Count();

            if (documents != null)
            {
                var list1 = documents.Select(x => new PlacementsModel
                {
                    ID = x.ID,
                    active = x.active,
                    ClientID = x.ClientID,
                    FormTypeID = x.FormTypeID,
                    ClientName = x.Client != null ? x.Client.ClientName : null,
                    FormTypeName = x.FormType != null ? x.FormType.FormType1 : null,
                    RequestsCount = x.Requests != null ? x.Requests.Count : 0
                });

                switch (sortCol)
                {
                    case "ClientName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.ClientName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.ClientName).ToList();
                        }
                        break;
                    case "FormTypeName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.FormTypeName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.FormTypeName).ToList();
                        }
                        break;
                    case "RequestsCount":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.RequestsCount).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.RequestsCount).ToList();
                        }
                        break;
                    default:
                        break;
                }

                model.placements = list1.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
            else
            {
                model.placements = new List<PlacementsModel>();
            }


            //model.documents = documents == null ? new List<DocumentsModel>() : documents?.Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength)?.Select(x => new DocumentsModel
            //{
            //    Id = x.ID,
            //    DocumentName = x.DocumentName,
            //    FileName = x.FileName,
            //    StateCodeId = x.StateCodeID,
            //    ProjectType = x.ProjectType,
            //    FormType = x.FormType,
            //    ClientId = x.ClientID,
            //    PracticeAreaId = x.PracticeAreaID,
            //    WFJFileNbr = x.WFJFileNbr,
            //    Days = x.Days,
            //    Description = x.Description,
            //    CategoryId = x.CategoryID,
            //    EmployeeCategoryId = x.EmployeeCategoryID,
            //    DocumentTypeId = x.DocumentTypeID,
            //    ProjectTypeId = x.ProjectTypeID,
            //    FormTypeId = x.FormTypeID,
            //    SeqNo = x.SeqNo,
            //    State = x.Code?.Value,
            //    DocumentType = x.Code1?.Value,
            //    ClientName = GetClientName(string.Join(", ", x.documentClients.Where(y => !string.IsNullOrEmpty(y.Client.ClientName)).Select(y => y.Client.ClientName))),
            //    CurrentUserType = Convert.ToInt32(HttpContext.Current.Session["UserType"]),
            //    DocumentFullPath = !string.IsNullOrEmpty(x.FileName) ? filePath + x.FileName : "",
            //    PracticeAreaName = x.PracticeArea?.PracticeAreaName
            //}).ToList();
            return model;
        }

    }
}
