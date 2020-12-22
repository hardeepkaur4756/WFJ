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
using WFJ.Service.Model;
using WFJ.Helper;
using System.Web;
using System.IO;

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

        public ManagePlacementsModel GetPlacements(UserType userType, int clientId, int formTypeId, string searchKeyword, DataTablesParam param, string sortDir, string sortCol, int pageNo, int? ClientUserId)
        {
            ManagePlacementsModel model = new ManagePlacementsModel();
            var documents = _formSearchRepository.GetFormList(clientId, formTypeId, ClientUserId);

            bool activeOnly = userType != UserType.SystemAdministrator;

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
                    RequestsCount = _requestsRepository.GetFormActiveRequestsCount(x.ID, activeOnly)
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
                FormData = Convert.ToInt32(requestId) > 0 ? x.FormDatas.Where(d => d.RequestID == requestId).Select(d => new FormDataViewModel
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
            if (form.ClientID != null)
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
                ClientName = form.Client != null ? form.Client.ClientName : null,
                ClientLevelName = form.Client != null ? form.Client.LevelName : null,
                ClientNumber = form.ClientNumber,
                CustomerNameFieldID = form.CustomerNameFieldID,
                DefaultRequestorID = form.DefaultRequestorID,
                FormName = form.FormName,
                FormTypeID = form.FormTypeID,
                FormTypeName = form.FormType != null ? form.FormType.FormType1 : null,
                hasCollector = form.hasCollector,
                ID = form.ID,
                JobNumberFieldID = form.JobNumberFieldID,
                TotalPaymentsFieldID = form.TotalPaymentsFieldID,
                WFJFileNbrFieldID = form.WFJFileNbrFieldID,
                Client = form.Client == null ? null : new ClientModel
                {
                    //autoShowFiles = form.Client.autoShowFiles == null ? (byte)0 : form.Client.autoShowFiles.Value,
                    ClientName = form.Client.ClientName,
                    ContactName = form.Client.ContactName,
                    Telephone = form.Client.Telephone,
                    EMail = form.Client.EMail,
                    Address1 = form.Client.Address1,
                    Address2 = form.Client.Address2,
                    City = form.Client.City,
                    State = form.Client.State,
                    ClientNotes = form.Client.clientNotes
                }
            };

            return formObj;
        }

        public List<SelectListItem> GetRequestorsDropdown(int FormID, int? requestorId, UserType userType)
        {
            List<SelectListItem> requestors = new List<SelectListItem>();

            /// for requestor
            if (Convert.ToInt32(requestorId) > 0)
            {
                IUserRepository _userRepo = new UserRepository();
                var user = _userRepo.GetById(requestorId);
                string name = user.FirstName + " " + user.LastName;
                if (!string.IsNullOrEmpty(name))
                {
                    requestors.Add(new SelectListItem
                    {
                        Text = name,
                        Value = user.UserID.ToString()
                    });
                }
            }

            var userRquestor = GetUsersByFormID(FormID).Where(x => x.User.Active == 1).Select(x => new SelectListItem
            {
                Text = x.User.FirstName + " " + x.User.LastName,
                Value = x.UserID.ToString()
            }).Where(x => x.Text.Trim() != "").OrderBy(x => x.Text).ToList();

            requestors.AddRange(userRquestor);

            /// for WFJ user only
            if (userType == UserType.SystemAdministrator)
            {
                IUserClientRepository _userClientRepo = new UserClientRepository();
                var userClient = _userClientRepo.GetByClientID(1);
                var wfjRequestor = userClient.Where(x => x.User.Active == 1).Select(x => new SelectListItem
                {
                    Text = x.User.FirstName + " " + x.User.LastName,
                    Value = x.UserID.ToString()
                }).Where(x => x.Text.Trim() != "");
                requestors.AddRange(wfjRequestor);
            }
            requestors = requestors.GroupBy(x => x.Value).Select(x => x.First()).ToList();
            return requestors;
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
                        active = 1,
                        FormID = savePlacementViewModel.FormId,
                        Requestor = savePlacementViewModel.RequestorId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.RequestorId),
                        AssignedAttorney = savePlacementViewModel.AttorneyId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.AttorneyId),
                        AssignedCollectorID = savePlacementViewModel.CollectorId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.CollectorId),
                        StatusCode = savePlacementViewModel.StatusId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.StatusId),
                        LevelID = savePlacementViewModel.LevelID,
                        AssignedAdminStaffID = savePlacementViewModel.AssignedAdminStaffID,
                        RequestDate = !string.IsNullOrEmpty(savePlacementViewModel.RequestDate) ? Convert.ToDateTime(savePlacementViewModel.RequestDate) : nullable,
                        CompletionDate = !string.IsNullOrEmpty(savePlacementViewModel.WFJFileCloseDate) ? Convert.ToDateTime(savePlacementViewModel.WFJFileCloseDate) : nullable
                    };
                    requestResult = _requestsRepo.Add(request);
                }
                else
                {
                    /// update request
                    var request = _requestsRepo.GetById(savePlacementViewModel.RequestId);
                    request.Requestor = savePlacementViewModel.RequestorId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.RequestorId);
                    request.AssignedAttorney = savePlacementViewModel.AttorneyId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.AttorneyId);
                    request.AssignedCollectorID = savePlacementViewModel.CollectorId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.CollectorId);
                    request.StatusCode = savePlacementViewModel.StatusId == null ? (int?)null : Convert.ToInt32(savePlacementViewModel.StatusId);
                    request.LevelID = savePlacementViewModel.LevelID;
                    request.AssignedAdminStaffID = savePlacementViewModel.AssignedAdminStaffID;
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
            sendMail(savePlacementViewModel, requestId);
            return requestId;
        }

        public void sendMail(SavePlacementViewModel savePlacementViewModel, int requestId)
        {
            IUserRepository _userRepo = new UserRepository();
            IFormFieldsRepository _formFieldRepo = new FormFieldsRepository();
            IClientRepository _clientRepo = new ClientRepository();
            var request = _requestsRepository.GetRequestWithDetail(requestId);
            string message = string.Empty;
            string messageInRed = string.Empty;
            string subject = string.Empty;
            string clientName = string.Empty;
            string status = "";
            List<string> emailTo = new List<string>();
            List<string> cc = new List<string>();

            var form = _formSearchRepository.GetById(savePlacementViewModel.FormId);
            var client = _clientRepo.GetById(form.ClientID);

            var statusCode = _statusCodesRepo.GetByStatusCodeAndFormId(request.StatusCode.Value, request.FormID.Value);
            status = statusCode.Description;

            var formfields = _formFieldRepo.GetFormFieldsByFormID(savePlacementViewModel.FormId).Where(x => x.EMailField == 1).ToList();
            string fieldsTr = string.Empty;
            foreach (var field in formfields)
            {
                var formData = field.FormDatas.FirstOrDefault(x => x.RequestID == requestId);
                fieldsTr += "<tr><td class=\"bodycopy width-cus-50\">" + field.FieldName + ":</td><td class=\"bodycopy width-cus-50\">" + formData?.FieldValue + "</td><tr/>";
            }


            string dirpath = HttpContext.Current.Server.MapPath("/EmailTemplate");
            string xlsTemplatePath = dirpath + "/BaseTemplate.html";
            string emailTemplate = File.ReadAllText(xlsTemplatePath);

            StringBuilder sb1 = new StringBuilder();
            sb1.Append(emailTemplate);
            sb1.Replace("[Status]", status);
            sb1.Replace("[Requestor]", request.User1 != null ? request.User1.FirstName + " " + request.User1.LastName : "");
            sb1.Replace("[Attorney]", request.Personnel != null ? request.Personnel.FirstName + " " + request.Personnel.LastName : "");
            sb1.Replace("[FormFields]", fieldsTr);            

            if (statusCode.StatusLevel == 0)
            {
                message = "Thank you for your " + form.FormName + " Request. ";
                messageInRed = "This information has been forwarded to the Wagner, Falconer & Judd staff for processing.";
                subject = form.FormName + " for " + client.ClientName + " ** New Request **";
            }
            else if (statusCode.StatusLevel == 1)
            {
                message = "This " + form.FormName + " Request has been reviewed by Wagner, Falconer & Judd.";
                messageInRed = "You will be contacted for any follow-up questions by Wagner, Falconer & Judd.";
                subject = form.FormName + " for " + client.ClientName + " ** Active **";
                IRequestNotesRepository _requestNotes = new RequestNotesRepository();
                var notes = _requestNotes.GetRequestNotes(requestId, true);
                string xlsTemplatePath2 = dirpath + "/RequestNotesList.html";
                string noteHtml = File.ReadAllText(xlsTemplatePath2);
                foreach (var note in notes.ToList())
                {
                    noteHtml += noteHtml.Replace("[NoteDate]", note.NotesDate.Value.ToString("MM/dd/yyyy")).Replace("[Author]", note.User.FirstName + " " + note.User.LastName)
                                   .Replace("[Notes]", note.Notes);
                }
                sb1.Replace("[NotesList]", noteHtml);
            }
            else if (statusCode.StatusLevel == 2)
            {
                message = "This " + form.FormName + " Request has been completed.";
                subject = form.FormName + " for " + client.ClientName + " ** Completed **";
            }

            sb1.Replace("[Message]", message);
            sb1.Replace("[MessageInRed]", messageInRed);

            /// email to
            var requestorEmail = request.User1.EMail;
            emailTo.Add(requestorEmail);
            IPersonnelsRepository _personnelsRepo = new PersonnelsRepository();
            var personnel = _personnelsRepo.GetEmailByPersonelRequestId(requestId);
            if (!string.IsNullOrEmpty(personnel?.EMail))
            {
                emailTo.Add(personnel?.EMail);
            }

            IEmailCopiesRepository _emailCopiesRepository = new EmailCopiesRepository();
            var usersEmail = _emailCopiesRepository.GetUsers().Where(x => x.EMail != null && x.EMail != "").Select(x => x.EMail).ToList();
            emailTo.AddRange(usersEmail);

            if (form.FormName.ToLower().Trim() == "wfj helpdesk")
            {
                emailTo.Add("mfischer@wfjlawfirm.com");
            }

            if (statusCode.StatusCode1 == 0)
            {
                IPersonnelClientsRepository _personnelClientsRepo = new PersonnelClientsRepository();
                if (form.ClientID != null)
                {
                    var personnels = _personnelClientsRepo.GetPersonnelsByClientID(Convert.ToInt32(form.ClientID));
                    emailTo.AddRange(personnels.Where(x => x.EMail != null && x.EMail != "").Select(x => x.EMail).ToList());
                }
                cc.Add("Reception@wfjlawfirm.com");
            }
            else
            {
                if ((statusCode.StatusCode1 >= 1 && statusCode.StatusCode1 <= 5) && form.FormTypeID == 10)
                {
                    cc.Add("mfischer@wfjlawfirm.com");
                    cc.Add("myoung @wfjlawfirm.com");
                }
                if (form.ClientID == 91 || form.ClientID == 97 || form.ClientID == 96 || form.ClientID == 102 || form.ClientID == 95 || form.ClientID == 109)
                {
                    emailTo.Add("sfalconer@wfjlawfirm.com");
                }
            }

            if (form.FormName.ToLower().Trim() != "lien request")
            {
                emailTo.Remove("MGeislinger@wfjlawfirm.com");
            }
            emailTo = emailTo.Distinct().ToList();
            var emails = string.Join(";", emailTo); 
            EmailHelper.SendMail(emails, subject, sb1.ToString(),true);
        }

        public SummaryInformation GetSummaryInformation(ClientModel clientModel, ProfileViewModel userDetail)
        {
            SummaryInformation summaryInformation = new SummaryInformation();
            #region ClientDetail
            summaryInformation.Clients = new Detail
            {
                Name = clientModel.ClientName,
                Address = GetAddress(clientModel.Address1, clientModel.Address2, clientModel.City, clientModel.State, clientModel.PostalCode),
                Email = clientModel.EMail,
                Phone = clientModel.Telephone,
                Contact = clientModel.ContactName
            };
            #endregion


            #region Requestor    
            if (userDetail != null)
            {
                var address1 = !string.IsNullOrEmpty(userDetail.Address1) ? userDetail.Address1 : clientModel.Address1;
                var address2 = !string.IsNullOrEmpty(userDetail.Address2) ? userDetail.Address2 : clientModel.Address2;
                var city = !string.IsNullOrEmpty(userDetail.City) ? userDetail.City : clientModel.City;
                var state = !string.IsNullOrEmpty(userDetail.State) ? userDetail.State : clientModel.State;
                var postalCode = !string.IsNullOrEmpty(userDetail.PostalCode) ? userDetail.PostalCode : clientModel.PostalCode;
                var name = userDetail.FirstName + " " + userDetail.LastName;
                if (string.IsNullOrEmpty(name))
                {
                    name = clientModel.ClientName;
                }

                summaryInformation.Requestors = new Detail
                {
                    Name = name,
                    Address = GetAddress(address1, address2, city, state, postalCode),
                    Email = !string.IsNullOrEmpty(userDetail.Email) ? userDetail.Email : clientModel.EMail,
                    Phone = !string.IsNullOrEmpty(userDetail.Telephone) ? userDetail.Telephone : clientModel.Telephone,
                };
            }
            else
            {
                summaryInformation.Requestors = new Detail
                {
                    Name = clientModel.ClientName,
                    Address = GetAddress(clientModel.Address1, clientModel.Address2, clientModel.City, clientModel.State, clientModel.PostalCode),
                    Email = clientModel.EMail,
                    Phone = clientModel.Telephone,
                };
            }

            #endregion
            return summaryInformation;
        }

        public string GetAddress(string address1, string address2, string city, string state, string postalCode)
        {
            string address = string.Empty;
            if (!string.IsNullOrEmpty(address1))
            {
                address += address1;
            }
            if (!string.IsNullOrEmpty(address2))
            {
                if (!string.IsNullOrEmpty(address1))
                {
                    address += ", " + address2;
                }
                else
                {
                    address += "<br/>" + address2;
                }
            }
            if (!string.IsNullOrEmpty(city))
            {
                address += "<br/>" + city;
            }
            if (!string.IsNullOrEmpty(state))
            {
                if (!string.IsNullOrEmpty(city))
                {
                    address += ", " + state;
                }
                else
                {
                    address += "<br/>" + state;
                }
            }
            if (!string.IsNullOrEmpty(postalCode))
            {
                if (!string.IsNullOrEmpty(city) || !string.IsNullOrEmpty(state))
                {
                    address += " " + postalCode;
                }
                else
                {
                    address += "<br/>" + postalCode;
                }
            }
            return address;
        }

        public void sendDocumentMail(int requestId, string uploadType, string fileName)
        {
            IRequestsRepository _requestsRepo = new RequestsRepository();
            var request = _requestsRepo.GetRequestWithDetail(requestId);
            string emailto = string.Empty;
            string subject = string.Empty;
            string documentHtml = string.Empty;
            if (uploadType == "add")
            {
                subject = " WFJ upload new document";
                documentHtml = fileName + " is uploaded";
            }
            else
            {
                subject = " WFJ remove document";
                documentHtml = fileName + " is deleted";
            }

            if (!string.IsNullOrEmpty(request.Personnel?.EMail))
            {
                emailto = request.Personnel?.EMail;
            }
            if (!string.IsNullOrEmpty(request.User1?.EMail))
            {
                if (string.IsNullOrEmpty(emailto))
                {
                    emailto = request.User1?.EMail;
                }
                else
                {
                    emailto = emailto + "," + request.User1?.EMail;
                }
            }

            string dirpath = HttpContext.Current.Server.MapPath("/EmailTemplate");
            string xlsTemplatePath = dirpath + "/RequestNotes.html";
            string emailTemplate = File.ReadAllText(xlsTemplatePath);

            StringBuilder sb1 = new StringBuilder();
            sb1.Append(emailTemplate);
            sb1.Replace("[Requestor]", request.User1 != null ? request.User1.FirstName + " " + request.User1.LastName : "");
            sb1.Replace("[Attorney]", request.Personnel != null ? request.Personnel.FirstName + " " + request.Personnel.LastName : "");

            string xlsTemplatePath2 = dirpath + "/RequestNotesList.html";
            string html = File.ReadAllText(xlsTemplatePath2);
            html = html.Replace("[NoteDate]", DateTime.Now.ToString("MM/dd/yyyy")).Replace("[Author]", request.User.FirstName + " " + request.User.LastName)
                                .Replace("[Notes]", documentHtml);

            sb1.Replace("[NotesList]", html);
            EmailHelper.SendMail(emailto, subject, sb1.ToString());
        }
    }
}
