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
        IStatusCodesRepository _statusCodesRepo = new StatusCodesRepository();
        private IFormsRepository _formSearchRepository = new FormsRepository();
        private IRequestsRepository _requestsRepository = new RequestsRepository();

        public List<SelectListItem> GetAllForms()
        {
            IFormsRepository formsRepo = new FormsRepository();
            List<SelectListItem> fornList = new List<SelectListItem>();
            fornList = formsRepo.GetAll().Where(x => !string.IsNullOrEmpty(x.FormName)).Select(x => new SelectListItem() { Text = x.FormName, Value = x.ID.ToString() }
                ).ToList();
            return fornList;
        }

        public ManagePlacementsModel GetPlacements(int clientId, int formTypeId, string searchKeyword, DataTablesParam param, string sortDir, string sortCol, int pageNo, int? ClientUserId)
        {
            ManagePlacementsModel model = new ManagePlacementsModel();
            var documents = _formSearchRepository.GetFormList(clientId, formTypeId, ClientUserId);


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
                    RequestsCount = _requestsRepository.GetFormActiveRequestsCount(x.ID)
                }).AsEnumerable();

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


            return model;
        }


        public List<FormFieldViewModel> GetFormFieldsByForm(int FormID, int? requestId)
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
                FormData = Convert.ToInt32(requestId) > 0 ? x.FormDatas.Where(d=>d.RequestID == requestId).Select(d => new FormDataViewModel
                {
                    currencyID = d.currencyID,
                    FieldValue = d.FieldValue,
                    FormFieldID = d.FormFieldID,
                    ID = d.ID,
                    RequestID = d.RequestID
                }).FirstOrDefault() : null,
                FormAddressData = Convert.ToInt32(requestId) > 0 ? x.FormAddressDatas.Where(d => d.RequestID == requestId).Select(a => new FormAddressDataViewModel
                {
                    AddressLine1 = a.AddressLine1,
                    AddressLine2 = a.AddressLine2,
                    AddressLine3 = a.AddressLine3,
                    City = a.City,
                    Country = a.Country,
                    FormFieldID = a.FormFieldID,
                    ID = a.ID,
                    RequestID = a.RequestID,
                    State = a.State,
                    ZipCode = a.ZipCode
                }).FirstOrDefault() : null,

                FieldSize = x.fieldSize == null ? null : new FieldSizeViewModel
                {
                    fieldSize1 = x.fieldSize.fieldSize1,
                    fieldSizeID = x.fieldSize.fieldSizeID,
                    htmlCode = x.fieldSize.htmlCode,
                    seqNo = x.fieldSize.seqNo
                },
                FormSelectionLists = x.FormSelectionLists == null ? new List<FormSelectionListViewModel>() : x.FormSelectionLists.Select(s => new FormSelectionListViewModel
                {
                    Code = s.Code,
                    FormFieldID = s.FormFieldID,
                    ID = s.ID,
                    SeqNo = s.SeqNo,
                    TextValue = s.TextValue
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

        public FormModel GetFormById(int FormID)
        {
            var form = _formSearchRepository.GetFormDetailByID(FormID);
            FormModel formObj = new FormModel
            {
                active = form.active,
                AccountBalanceFieldID = form.AccountBalanceFieldID,
                CustomerAccountFieldID = form.CustomerAccountFieldID,
                hasAdmin = form.hasAdmin,
                ClientID = form.ClientID,
                ClientName = form.Client != null ? form.Client.ClientName :null,
                ClientNumber = form.ClientNumber,
                CustomerNameFieldID = form.CustomerNameFieldID,
                DefaultRequestorID = form.DefaultRequestorID,
                FormName = form.FormName,
                FormTypeID = form.FormTypeID,
                FormTypeName = form.FormType != null? form.FormType.FormType1 : null,
                hasCollector = form.hasCollector,
                ID = form.ID,
                JobNumberFieldID = form.JobNumberFieldID,
                TotalPaymentsFieldID = form.TotalPaymentsFieldID,
                WFJFileNbrFieldID = form.WFJFileNbrFieldID,
                Client = form.Client == null ? null : new ClientModel
                {
                    //autoShowFiles = form.Client.autoShowFiles == null ? (byte)0 : form.Client.autoShowFiles.Value,
                    ClientName = form.Client.ClientName
                }
            };

            return formObj;
        }

        public List<SelectListItem> GetRequestorsDropdown(int FormID)
        {
            return GetUsersByFormID(FormID).Where(x => x.User.Active == 1).Select(x => new SelectListItem
            {
                Text = x.User.FirstName + " " + x.User.LastName,
                Value = x.UserID.ToString()
            }).Where(x => x.Text.Trim() != "").OrderBy(x => x.Text).ToList();
        }

        public List<SelectListItem> GetCollectorsDropdown()
        {
            IUserRepository _userRepo = new UserRepository();
            var collectors = _userRepo.GetAll().Where(x => x.IsCollector == 1 && x.Active == 1).Select(x => new SelectListItem
            {
                Text = x.FirstName + " " + x.LastName,
                Value = x.UserID.ToString()
            }).Where(x => x.Text.Trim() != "").OrderBy(x => x.Text).ToList();
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

        public int SavePlacements(SavePlacementViewModel savePlacementViewModel)
        {
            int requestId = 0;
            List<FormData> formDatas = new List<FormData>();
            List<FormAddressData> formAddressDatas = new List<FormAddressData>();
            if (savePlacementViewModel != null)
            {
                IRequestsRepository _requestsRepo = new RequestsRepository();
                IFormDataRepository _formDataRepo = new FormDataRepository();
                IFormAddressDataRepository _formAddressDataRepo = new FormAddressDataRepository();
                Request requestResult = new Request();
                DateTime? nullable = null;
                if (savePlacementViewModel.RequestId == 0)
                {
                    

                    /// Add request
                    Request request = new Request
                    {
                        //StatusCode = statuscodes.Count() > 0 ? statuscodes.FirstOrDefault() : null,// activeStatusCode != null? activeStatusCode.Value : (int?)null,
                        FormID = savePlacementViewModel.FormId,
                        Requestor = Convert.ToInt32(savePlacementViewModel.RequestorId),
                        AssignedAttorney = Convert.ToInt32(savePlacementViewModel.AttorneyId),
                        AssignedCollectorID = Convert.ToInt32(savePlacementViewModel.CollectorId),
                        StatusCode = Convert.ToInt32(savePlacementViewModel.StatusId),
                        RequestDate = !string.IsNullOrEmpty(savePlacementViewModel.RequestDate) ? Convert.ToDateTime(savePlacementViewModel.RequestDate) : nullable,
                        CompletionDate = !string.IsNullOrEmpty(savePlacementViewModel.WFJFileCloseDate) ? Convert.ToDateTime(savePlacementViewModel.WFJFileCloseDate) : nullable
                    };
                    requestResult = _requestsRepo.Add(request);
                }
                else
                {
                    /// update request
                    var request = _requestsRepo.GetById(savePlacementViewModel.RequestId);
                    request.Requestor = Convert.ToInt32(savePlacementViewModel.RequestorId);
                    request.AssignedAttorney = Convert.ToInt32(savePlacementViewModel.AttorneyId);
                    request.AssignedCollectorID = Convert.ToInt32(savePlacementViewModel.CollectorId);
                    request.StatusCode = Convert.ToInt32(savePlacementViewModel.StatusId);
                    request.RequestDate = !string.IsNullOrEmpty(savePlacementViewModel.RequestDate) ? Convert.ToDateTime(savePlacementViewModel.RequestDate) : nullable;
                    request.CompletionDate = !string.IsNullOrEmpty(savePlacementViewModel.WFJFileCloseDate) ? Convert.ToDateTime(savePlacementViewModel.WFJFileCloseDate) : nullable;
                    requestResult = _requestsRepo.Update(request);
                }

                requestId = requestResult.ID;

                if (savePlacementViewModel.RequestId > 0)
                {
                    var formAddressData = _formAddressDataRepo.GetByRequestId(savePlacementViewModel.RequestId).ToList();
                    _formAddressDataRepo.Delete(formAddressData);

                    var formData = _formDataRepo.GetByRequestId(savePlacementViewModel.RequestId).ToList();
                    _formDataRepo.Delete(formData);
                }
                foreach (var value in savePlacementViewModel.FieldValue)
                {
                    if (value.FieldId > 0)
                    {
                        if (!string.IsNullOrEmpty(value.FieldValue) && value.AddressValue == null)
                        {
                            FormData formData = new FormData
                            {
                                RequestID = requestResult.ID,
                                FormFieldID = value.FieldId,
                                FieldValue = value.FieldValue,
                                currencyID = value.CurrencyId
                            };
                            formDatas.Add(formData);
                        }
                        else if (value.FieldValue == null && value.AddressValue != null)
                        {
                            // for address
                            if (!string.IsNullOrEmpty(value.AddressValue.Address1) || !string.IsNullOrEmpty(value.AddressValue.Address2)
                                || !string.IsNullOrEmpty(value.AddressValue.City) || !string.IsNullOrEmpty(value.AddressValue.State)
                                || !string.IsNullOrEmpty(value.AddressValue.Zipcode) || !string.IsNullOrEmpty(value.AddressValue.Country))
                            {
                                FormAddressData formAddressData = new FormAddressData
                                {
                                    RequestID = requestResult.ID,
                                    FormFieldID = value.FieldId,
                                    AddressLine1 = value.AddressValue.Address1,
                                    AddressLine2 = value.AddressValue.Address2,
                                    City = value.AddressValue.City,
                                    State = value.AddressValue.State,
                                    ZipCode = value.AddressValue.Zipcode,
                                    Country = value.AddressValue.Country
                                };
                                formAddressDatas.Add(formAddressData);
                            }
                        }
                    }
                }
                _formDataRepo.AddList(formDatas.AsEnumerable());
                _formAddressDataRepo.AddList(formAddressDatas.AsEnumerable());
            }
            return requestId;
        }
    }
}
