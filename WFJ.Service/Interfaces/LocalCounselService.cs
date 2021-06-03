using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WFJ.Models;
using WFJ.Repository;
using WFJ.Repository.EntityModel;
using WFJ.Repository.Interfaces;

namespace WFJ.Service.Interfaces
{
    public class LocalCounselService : ILocalCounselService
    {
        IAssociateCounselRepository _associateCouncelRepo = new AssociateCounselRepository();
        IFormsRepository _formsRepositoryRepo = new FormsRepository();
        private IErrorLogService _errorLogService = new ErrorLogService();
        IPersonnelsRepository _personalRepo = new PersonnelsRepository();
        IFormDataRepository _formDataRepo = new FormDataRepository();
        IFormFieldsRepository _formfieldRepo = new FormFieldsRepository();
        IClientRepository _clientRepo = new ClientRepository();
        IRequestsRepository _requestsRepo = new RequestsRepository();
        IPersonnelRequestRepository _personnelRequestRepo  = new PersonnelRequestRepository();
        IStatusCodesRepository _statusCodesRepo = new StatusCodesRepository();
        public ManageLocalCounselModel GetLocalCounsels(string firmName, string attorneyName, string contactName, string city, string state, string country, DataTablesParam param, string sortDir, string sortCol, int pageNo,int wfjAttorneyId)
        {
            ManageLocalCounselModel model = new ManageLocalCounselModel();
            var counsels = _associateCouncelRepo.GetAssociateCounselList(firmName, attorneyName,contactName, city, state, country, wfjAttorneyId);

            model.totalLocalCounselsCount = counsels?.Count();


            if (counsels != null)
            {

                var list1 = counsels.Select(x => new LocalCounselModel
                {
                    Id = x.FirmID,
                    FirmName = (_personalRepo.GetPersonnelByFirmId(x.FirmID).Where(y => y.Request != null).ToList()).Count > 0 
                    ? x.FirmName + " (" + (_personalRepo.GetPersonnelByFirmId(x.FirmID).Where(y => y.Request != null).ToList()).Count + ")"
                    : x.FirmName,
                    //no column in local DB need to check live DB
                    ContactName = x.Name,
                    AttorneyName = x?.Name,
                    City = x.City,
                    State = x.State,
                    Country = x.Country,
                    DoNotUse = Convert.ToInt32(x.DoNotUse)
                }).AsEnumerable();

                switch (sortCol)
                {
                    case "FirmName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.FirmName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.FirmName).ToList();
                        }
                        break;
                    case "ContactName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.ContactName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.ContactName).ToList();
                        }
                        break;
                    case "AttorneyName":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.AttorneyName).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.AttorneyName).ToList();
                        }
                        break;
                    case "City":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.City).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.City).ToList();
                        }
                        break;
                    case "State":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.State).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.State).ToList();
                        }
                        break;
                    case "Country":
                        if (sortDir == "asc")
                        {
                            list1 = list1.OrderBy(x => x.Country).ToList();
                        }
                        if (sortDir == "desc")
                        {
                            list1 = list1.OrderByDescending(x => x.Country).ToList();
                        }
                        break;
                    default:
                        break;
                }
                
                model.localCounsels = list1.OrderBy(x =>x.DoNotUse).Skip((pageNo - 1) * param.iDisplayLength).Take(param.iDisplayLength).ToList();
            }
            else
            {
                model.localCounsels = new List<LocalCounselModel>();
            }


            return model;
        }

        public int SaveLocalCounsel(AddLocalCounselViewModel addLocalCounselViewModel)
        {
            int firmId = 0;
            var associateResult = new AssociateCounsel();
            if (addLocalCounselViewModel != null)
            {
                IAssociateCounselRepository associateCounselRepo = new AssociateCounselRepository();
               
                if (addLocalCounselViewModel.FirmId == 0)
                { 
                    /// Add counsel
                    AssociateCounsel associateCounsel = new AssociateCounsel
                    {
                        FirmName = addLocalCounselViewModel.FirmName,
                        Telephone1 = addLocalCounselViewModel.PhoneNumber,
                        Name = addLocalCounselViewModel.AttorneyName,
                        Telephone2 = addLocalCounselViewModel.DirectLine,
                        AddressLine1 = addLocalCounselViewModel.Address,
                        Fax = addLocalCounselViewModel.Fax,
                        AddressLine2 = addLocalCounselViewModel.Suite,
                        Email = addLocalCounselViewModel.Email,
                        City = addLocalCounselViewModel.City,
                        Website = addLocalCounselViewModel.Website,
                        State = addLocalCounselViewModel.State,
                        FederalTaxID = addLocalCounselViewModel.FederalTaxId,
                        ZipCode = addLocalCounselViewModel.ZipCode,
                        Check = addLocalCounselViewModel.Check,
                        Country = addLocalCounselViewModel.Country,
                        W9 = addLocalCounselViewModel.W9,
                        ALQ = addLocalCounselViewModel.ALQ,
                        GB = addLocalCounselViewModel.GeneralBar,
                        WH = addLocalCounselViewModel.WrightHolmess,
                       DoNotUse = Convert.ToByte(addLocalCounselViewModel.DoNotUse),
                       Notes = addLocalCounselViewModel.Notes
                    };
                    associateResult = associateCounselRepo.Add(associateCounsel);
                }
                else
                {
                    /// update counsel
                    var associateCounsel = associateCounselRepo.GetById(addLocalCounselViewModel.FirmId);
                    associateCounsel.FirmName = addLocalCounselViewModel.FirmName;
                    associateCounsel.Name = addLocalCounselViewModel.AttorneyName;
                    associateCounsel.Telephone1 = addLocalCounselViewModel.PhoneNumber;
                    associateCounsel.Telephone2 = addLocalCounselViewModel.DirectLine;
                    associateCounsel.AddressLine1 = addLocalCounselViewModel.Address;
                    associateCounsel.AddressLine2 = addLocalCounselViewModel.Suite;
                    associateCounsel.Fax = addLocalCounselViewModel.Fax;
                    associateCounsel.Email = addLocalCounselViewModel.Email;
                    associateCounsel.City = addLocalCounselViewModel.City;
                    associateCounsel.Website = addLocalCounselViewModel.Website;
                    associateCounsel.State = addLocalCounselViewModel.State;
                    associateCounsel.FederalTaxID = addLocalCounselViewModel.FederalTaxId;
                    associateCounsel.ZipCode = addLocalCounselViewModel.ZipCode;
                    associateCounsel.Check = addLocalCounselViewModel.Check;
                    associateCounsel.Country = addLocalCounselViewModel.Country;
                    associateCounsel.W9 = addLocalCounselViewModel.W9;
                    associateCounsel.ALQ = addLocalCounselViewModel.ALQ;
                    associateCounsel.GB = addLocalCounselViewModel.GeneralBar;
                    associateCounsel.WH = addLocalCounselViewModel.WrightHolmess;
                    associateCounsel.DoNotUse = Convert.ToByte(addLocalCounselViewModel.DoNotUse);
                    associateCounsel.Notes = addLocalCounselViewModel.Notes;
                    associateResult = associateCounselRepo.Update(associateCounsel);
                }
                firmId = associateResult.FirmID;
                
            }
            return firmId;
        }

        public AddLocalCounselViewModel GetById(int firmId)
        {
            var addLocalCounselViewModel = new AddLocalCounselViewModel();
            IAssociateCounselRepository associateCounselRepo = new AssociateCounselRepository();
            var entity = associateCounselRepo.GetById(firmId);
            addLocalCounselViewModel = ConvertEntityToAddLocalCounsel(associateCounselRepo.GetById(firmId));
            return addLocalCounselViewModel;
        }
        private AddLocalCounselViewModel ConvertEntityToAddLocalCounsel(AssociateCounsel associateCounsel)
        {
            if (associateCounsel == null)
            {
                return new AddLocalCounselViewModel();
            }
            else
            {
                return new AddLocalCounselViewModel()
                {
                    FirmName = associateCounsel.FirmName,
                    PhoneNumber = associateCounsel.Telephone1,
                    AttorneyName = associateCounsel.Name,
                    DirectLine = associateCounsel.Telephone2,
                    Address = associateCounsel.AddressLine1,
                    Fax = associateCounsel.Fax,
                    Suite = associateCounsel.AddressLine2,
                    Email = associateCounsel.Email,
                    City = associateCounsel.City,
                    Website = associateCounsel.Website,
                    State = associateCounsel.State,
                    FederalTaxId = associateCounsel.FederalTaxID,
                    ZipCode = associateCounsel.ZipCode,
                    Check = Convert.ToBoolean(associateCounsel.Check),
                    Country = associateCounsel.Country,
                    W9 = Convert.ToBoolean(associateCounsel.W9),
                    ALQ = Convert.ToBoolean(associateCounsel.ALQ),
                    GeneralBar = Convert.ToBoolean(associateCounsel.GB),
                    WrightHolmess = Convert.ToBoolean(associateCounsel.WH),
                    DoNotUse = Convert.ToBoolean(associateCounsel.DoNotUse),
                    Notes = associateCounsel.Notes,
                };
            }
        }

        public List<FileInformation> GetFileInformation(int firmId)
        {
            List<FileInformation> list = new List<FileInformation>();
            var fileInformation = new FileInformation();
            var entities = _personalRepo.GetPersonnelByFirmId(firmId).Where(x=>x.Request != null).ToList();
            if(entities!=null)
            {
                foreach(var item in entities)
                {
                    var request = _requestsRepo.GetRequestWithDetail(Convert.ToInt32(item.RequestID));
                    var formFields = _formfieldRepo.GetFormFieldsByFormID(Convert.ToInt32(request.FormID));
                    var customerNameFieldId = formFields.FirstOrDefault(x => x.FieldName.ToLower().Trim() == "customer name")?.ID;
                    var wfjFileNoFieldId = formFields.FirstOrDefault(x => x.FieldName.ToLower().Trim() == "wfj file #")?.ID;
                    var formdata = _formDataRepo.GetByRequestId(Convert.ToInt32(item.RequestID));
                    string customerName = string.Empty;
                    string wfjFileNo = string.Empty;
                    if (Convert.ToInt32(customerNameFieldId) > 0)
                    {
                        customerName = formdata.FirstOrDefault(x => x.FormFieldID == customerNameFieldId).FieldValue;
                    }
                    if (Convert.ToInt32(wfjFileNoFieldId) > 0)
                    {
                        wfjFileNo = formdata?.FirstOrDefault(x => x.FormFieldID == wfjFileNoFieldId)?.FieldValue;
                    }
                    string clientName = _clientRepo.GetById(Convert.ToInt32(request?.Form?.Client.ID)).ClientName;
                    var statuscode = _statusCodesRepo.GetByStatusCodeAndFormId(request.StatusCode.Value, request.FormID.Value);
                        fileInformation = new FileInformation()
                        {
                            Client = clientName,
                            CustomerName = customerName,
                            WfjFile = wfjFileNo,
                            AttorneyName = item.AssociateName,
                            LienCollection = request?.Form?.FormName,
                            Status = statuscode?.Description,
                            Path = $"/Placements/AddPlacement?formId={request.FormID}&requestId={item.RequestID}"
                        };
                    list.Add(fileInformation);
            }
            }
            return list.OrderBy(x=>x.WfjFile).ToList();
        }

        public void DeleteAssociateCounse(int firmId)
        {
            var personnelRequest = _personalRepo.GetPersonnelRequestsByFirmId(firmId);
            foreach(var item in personnelRequest)
            {
                _personnelRequestRepo.Remove(item.ID);
            }
            _associateCouncelRepo.Remove(firmId);
        }

        public AssociateCounselModel GetByFirmId(int firmId)
        {
         var associateCounsel =   _associateCouncelRepo.GetById(firmId);
            if (associateCounsel == null)
            {
                return new AssociateCounselModel();
            }
            else
            {
                var associateCounselModel = new AssociateCounselModel();
                associateCounselModel.FirmName = associateCounsel.FirmName;
                associateCounselModel.Name = associateCounsel.Name;
                associateCounselModel.Telephone1 = associateCounsel.Telephone1;
                associateCounselModel.Telephone2 = associateCounsel.Telephone2;
                associateCounselModel.AddressLine1 = associateCounsel.AddressLine1;
                associateCounselModel.AddressLine1 = associateCounsel.AddressLine1;
                associateCounselModel.Fax = associateCounsel.Fax;
                associateCounselModel.Email = associateCounsel.Email;
                associateCounselModel.City = associateCounsel.City;
                associateCounselModel.Website = associateCounsel.Website;
                associateCounselModel.State = associateCounsel.State;
                associateCounselModel.FederalTaxID = associateCounsel.FederalTaxID;
                associateCounselModel.ZipCode = associateCounsel.ZipCode;
                associateCounselModel.Check = associateCounsel.Check;
                associateCounselModel.Country = associateCounsel.Country;
                associateCounselModel.W9 = associateCounsel.W9;
                associateCounselModel.ALQ = associateCounsel.ALQ;
                associateCounselModel.GB = associateCounsel.GB;
                associateCounselModel.DoNotUse = associateCounsel.DoNotUse;
                associateCounselModel.Notes = associateCounsel.Notes;
                associateCounselModel.fileNumber = associateCounsel.PersonnelRequests?.FirstOrDefault()?.fileNumber;
                associateCounselModel.localCounselStatus = associateCounsel.PersonnelRequests?.FirstOrDefault()?.localCounselStatus1?.localCounselStatus1;
                associateCounselModel.localCounselRate = associateCounsel.PersonnelRequests?.FirstOrDefault()?.localCounselRate;
                return associateCounselModel;
            }
            
        }
    }
}