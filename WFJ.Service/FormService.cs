using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.Interfaces;
using WFJ.Repository.EntityModel;
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


        public List<FormFieldViewModel> GetFormFieldsByForm(int FormID)
        {
            IFormFieldsRepository _formFieldRepository = new FormFieldsRepository();
            var formFieldList = _formFieldRepository.GetFormFieldsByFormID(FormID).Select(x => new FormFieldViewModel
            {
                AccessLevel = x.AccessLevel,
                AccountSummarySeqNo = x.AccountSummarySeqNo,
                EMailField = x.EMailField,
                EPDBFieldName = x.EPDBFieldName,
                FieldName = x.FieldName,
                FieldTypeID = x.FieldTypeID,
                FormID = x.FormID,
                formSectionID = x.formSectionID,
                ID = x.ID,
                Length = x.Length,
                ListSeqNo = x.ListSeqNo,
                Required = x.Required,
                SelectionColumn = x.SelectionColumn,
                SeqNo = x.SeqNo,
                showOnCalendar = x.showOnCalendar,
                SQLStatement = x.SQLStatement,
                rowNumber = x.rowNumber,
                FieldSize = x.fieldSize == null ? null : new FieldSizeViewModel
                {
                    fieldSize1 = x.fieldSize.fieldSize1,
                    fieldSizeID = x.fieldSize.fieldSizeID,
                    htmlCode = x.fieldSize.htmlCode,
                    seqNo = x.fieldSize.seqNo
                },
                FormSelectionLists = x.FormSelectionLists == null ? new List<FormSelectionListViewModel>() : x.FormSelectionLists.Select(s => new FormSelectionListViewModel { 
                Code = s.Code,
                FormFieldID =s.FormFieldID,
                ID = s.ID,
                SeqNo =s.SeqNo,
                TextValue = s.TextValue,
                }).AsEnumerable()
            }).ToList();
            return formFieldList;
        }

        public List<FormSectionViewModel> GetFormSections()
        {
            IFormSectionsRepository _formSectionRepoitory = new FormSectionsRepository();
            var sectionList = _formSectionRepoitory.GetAll().OrderBy(x => x.sequenceID).Select(x => new FormSectionViewModel
            {
                formSectionID = x.formSectionID,
                sectionName = x.sectionName,
                sequenceID = x.sequenceID
            }).ToList();

            return sectionList;
        }

        public List<UserClient> GetUsersByFormID(int FormID)
        {
            IUserClientRepository _userClientRepo = new UserClientRepository();
            var form = _formSearchRepository.GetById(FormID);

            List<UserClient> ClientUsers = new List<UserClient>();
            if(form.ClientID != null)
            ClientUsers = _userClientRepo.GetByClientID(form.ClientID.Value);

            return ClientUsers;
        }

        public List<SelectListItem> GetRequestorsDropdown(int FormID)
        {
            return GetUsersByFormID(FormID).Where(x => x.User.Active == 1 && x.User.FirstName != null && x.User.FirstName.Trim() != "").Select(x => new SelectListItem
            {
                Text = x.User.FirstName + " " + x.User.LastName,
                Value = x.UserID.ToString()
            }).OrderBy(x => x.Text).ToList();
        }

        public List<SelectListItem> GetCollectorsDropdown()
        {
            IUserRepository _userRepo = new UserRepository();
            var collectors = _userRepo.GetAll().Where(x => x.IsCollector == 1 && x.Active == 1 && x.FirstName != null && x.FirstName.Trim() != "").Select(x => new SelectListItem
                                {
                                    Text = x.FirstName + " " + x.LastName,
                                    Value = x.UserID.ToString()
                                }).OrderBy(x => x.Text).ToList();
            return collectors;
        }

        public List<SelectListItem> GetPersonnelsDropdown(int FormID)
        {
            IPersonnelClientsRepository _personnelClientRepo = new PersonnelClientsRepository();

            IUserClientRepository _userClientRepo = new UserClientRepository();
            var form = _formSearchRepository.GetById(FormID);

            List<SelectListItem> personnels = new List<SelectListItem>();
            if (form.ClientID != null)
                personnels = _personnelClientRepo.GetPersonnelsByClientID(form.ClientID.Value).Select(x => new SelectListItem
                {
                    Text = x.FirstName + " " + x.LastName,
                    Value = x.ID.ToString()
                }).OrderBy(x => x.Text).ToList();

            return personnels;
        }



    }
}
