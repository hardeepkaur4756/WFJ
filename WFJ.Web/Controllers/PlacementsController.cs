using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Service.Model;

namespace WFJ.Web.Controllers
{
    [CustomAttribute.AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
    public class PlacementsController : Controller
    {
        private IErrorLogService _errorLogService = new ErrorLogService();
        private IFormService _formService = new FormService();
        public IClientService _clientService = new ClientService();
        private IFormTypeService _formTypeService = new FormTypeService();
        private IUserService _userService = new UserService();
        private IRequestsService _requestsService = new RequestsService();
        private ICodesService _codeService = new CodesService();
        private IUserClientService _userClientService = new UserClientService();
        private ILevelService _levelService = new LevelService();
        private IRequestDocumentService _requestDocumentService = new RequestDocumentService();

        private int UserType = 0;
        private int UserId = 0;
        private int? UserAccess;

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                PlacementsViewModel model = new PlacementsViewModel();
                model.placementsFilterViewModel = new PlacementsFilterViewModel()
                {
                    client = UserType == (int)Web.Models.Enums.UserType.ClientUser ? _userClientService.GetUserClients((UserType)((byte)UserType), UserId, 1) : _clientService.GetActiveInactiveOrderedList((UserType)((byte)UserType)),
                    placementTypeModels = _formTypeService.GetFormTypesDropdown(),
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/Index", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new PlacementsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }

        }

        [HttpGet]
        public JsonResult GetPlacementList(DataTablesParam param, string sortDir, string sortCol, bool isFirstTime, int clientId = -1, int formTypeId = -1, string searchKeyword = "")
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                ManagePlacementsModel model = new ManagePlacementsModel();
                int pageNo = 1;
                if (param.iDisplayStart >= param.iDisplayLength)
                    pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;


                int? userSpecific = UserType == (int)Web.Models.Enums.UserType.ClientUser ? UserId : (Nullable<int>)null;

                if ((int)WFJ.Web.Models.Enums.UserType.SystemAdministrator != UserType || isFirstTime == false)
                    model = _formService.GetPlacements((UserType)((byte)UserType), clientId, formTypeId, searchKeyword, param, sortDir, sortCol, pageNo, userSpecific);
                else
                    model.placements = new List<PlacementsModel>();

                return Json(new
                {
                    aaData = model.placements,
                    param.sEcho,
                    iTotalDisplayRecords = model.totalPlacementsCount,
                    iTotalRecords = model.totalPlacementsCount,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/GetPlacementList", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });

            }
        }

        public ActionResult ViewPlacements(int id, string success)
        {
            ViewData["SuccessMessage"] = success;

            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                IUserService _userService = new UserService();
                IStatusCodesService _statusCodesService = new StatusCodesService();
                PlacementReuestsViewModel model = new PlacementReuestsViewModel();

                var form = _formService.GetFormById(id);
                model.ClientName = form.ClientName;
                model.FormType = form.FormTypeName;
                model.FormID = id;
                model.TableColumns = _requestsService.GetDatatableColumns(UserId, id, (UserType)((byte)UserType));
                model.AllColumnsList = _requestsService.GetAllcolumns(UserId, id, (UserType)((byte)UserType));

                model.placementReuestsFilterViewModel = new PlacementReuestsFilterViewModel()
                {
                    FormID = id,
                    Requestors = DropdownHelpers.PrependALL(_formService.GetRequestorsDropdown(id)),
                    RegionList = DropdownHelpers.PrependALL(form.ClientID == null ? new List<SelectListItem>() : _levelService.GetRegionsByClientID(form.ClientID.Value)), //_clientService.GetRegionsDropdown(),
                    Collectors = DropdownHelpers.PrependALL(_formService.GetCollectorsDropdown()),
                    StatusList = DropdownHelpers.PrependALL(_statusCodesService.GetByFormID(id)),
                    AssignedToList = DropdownHelpers.PrependALL(_formService.GetPersonnelsDropdown(id))
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/ViewPlacements", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new PlacementReuestsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }


        public ActionResult AddPlacement(int? formId, int? requestId, int? copy, string value = null)
        {
            try
            {
                if (!string.IsNullOrEmpty(value))
                {
                    var decodeKey = Util.Decode(value.ToString());
                    var decryptValue = decodeKey.Split('|');
                    formId = Convert.ToInt32(decryptValue[0]);
                    requestId = Convert.ToInt32(decryptValue[1]);
                }
                GetSessionUser(out UserId, out UserType, out UserAccess);
                var form = _formService.GetFormById(Convert.ToInt32(formId));
                int accountBalanceFieldId = Convert.ToInt32(form.AccountBalanceFieldID);
                var userDetail = _userService.GetById(UserId);
                var documentType = _codeService.GetAllByType("DOCTYPE");
                IStatusCodesService _statusCodesService = new StatusCodesService();
                ICurrenciesService _currenciesService = new CurrenciesService();
                AddEditPlacementsViewModel model = new AddEditPlacementsViewModel
                {
                    CurrencyDropdown = _currenciesService.GetCurrencyDropdown(),
                    FormSections = _formService.GetFormSections(),
                    FormFieldsList = _formService.GetFormFieldsByForm(Convert.ToInt32(formId), requestId),
                    Collectors = _formService.GetCollectorsDropdown(),
                    Requestors = _formService.GetRequestorsDropdown(Convert.ToInt32(formId)),
                    StatusList = _statusCodesService.GetByFormID(Convert.ToInt32(formId)),
                    AssignedAtorneys = _formService.GetPersonnelsDropdown(Convert.ToInt32(formId)),
                    RegionList = form.ClientID == null ? new List<SelectListItem>() : _levelService.GetRegionsByClientID(form.ClientID.Value),
                    AdminStaffList = form.hasAdmin == 1 ? _userService.GetAdminStaffDropdown() : new List<SelectListItem>(),
                    UserAccess = UserAccess,
                    UserType = UserType,
                    ClientId = Convert.ToInt32(form.ClientID),
                    isEditMode = Convert.ToInt32(requestId) > 0 && Convert.ToInt32(copy) == 0 ? true : false,
                    RequestorName = form.ClientName == null ? "Requestor" : form.ClientName,
                    FormDetail = form,
                    NotesSendToDropdown = new List<SelectListItem>(),
                    summaryInformation = _formService.GetSummaryInformation(form.Client, userDetail),
                    DocumentType = documentType
                };

                #region Bind PaymentInformation in Summary
                string customerPhone = string.Empty;
                decimal balanceDue = 0;
                model.summaryInformation.Payments = new PaymentDetail();
                model.summaryInformation.ClientNotes = new List<RequestNoteModel>();
                model.summaryInformation.FlagNotes = new List<RequestNoteModel>();
                var customerPhoneField = model.FormFieldsList.FirstOrDefault(x => x.FieldName.ToLower().Trim() == "customer phone");
                if (customerPhoneField != null)
                {
                    customerPhone = customerPhoneField.FormData?.FieldValue;
                }
                model.summaryInformation.Payments.CustomerPhone = string.IsNullOrEmpty(customerPhone) ? "-" : customerPhone;
                if (accountBalanceFieldId > 0)
                {
                    var formField = model.FormFieldsList.FirstOrDefault(x => x.ID == accountBalanceFieldId);
                    balanceDue = Convert.ToDecimal(formField.FormData?.FieldValue);
                }
                model.summaryInformation.Payments.BalanceDue = balanceDue;
                model.summaryInformation.Payments.LastPaymentDate = "-";
                model.summaryInformation.Payments.TotalPayment = balanceDue;
                model.summaryInformation.Payments.RemainingAmount = balanceDue;
                model.summaryInformation.Payments.isPaymentFieldShow = false;
                #endregion

                if (requestId == null)
                {
                    model.Request = new RequestViewModel { FormID = formId, RequestDateString = DateTime.Now.ToString("MM/dd/yyyy") };
                }
                else
                {
                    IRequestsService _requestService = new RequestsService();
                    model.Request = _requestService.GetByRequestId(requestId.Value);
                    model.summaryInformation.ClientNotes = model.Request.RequestNotes;
                    model.summaryInformation.FlagNotes = model.Request.RequestNotes.Where(x => x.flaggedNote == 1).ToList();
                    if (balanceDue > 0)
                    {
                        IPaymentService _paymentService = new PaymentService();
                        model.summaryInformation.Payments.isPaymentFieldShow = true;
                        var payments = _paymentService.GetByRequestId(requestId.Value);
                        double? totalPayment = 0;
                        if (payments.Any())
                        {
                            model.summaryInformation.Payments.LastPaymentDate = payments.OrderBy(x => x.PaymentDate).FirstOrDefault().PaymentDate.Value.ToString("MM/dd/yyyy");
                            totalPayment = payments.Sum(x => x.Amount);
                        }
                        model.summaryInformation.Payments.TotalPayment = Convert.ToDecimal(totalPayment);
                        model.summaryInformation.Payments.RemainingAmount = balanceDue - Convert.ToDecimal(totalPayment);
                    }
                    else
                    {
                        model.summaryInformation.Payments.isPaymentFieldShow = false;
                    }

                    RequestDocumentViewModel requestDocumentViewModel = new RequestDocumentViewModel();
                    requestDocumentViewModel.RequestDocumentDetails = _requestDocumentService.GetbyRequestId(requestId.Value);
                    requestDocumentViewModel.DocumentType = documentType;
                    model.requestDocumentViewModel = requestDocumentViewModel;

                    if (copy == 1)
                    {
                        model.isCopyMode = true;
                        model.Request.ID = 0;
                        foreach (var item in model.FormFieldsList)
                        {
                            if (item.FormData != null)
                            {
                                item.FormData.RequestID = null;
                            }
                            if (item.FormAddressData != null)
                            {
                                item.FormAddressData.RequestID = null;
                            }
                        }
                    }
                    else
                    {
                        IRequestNotesService _requestNotesService = new RequestNotesService();
                        model.NotesSendToDropdown = _requestNotesService.GetSendToDropdown(Convert.ToInt32(formId), requestId.Value);
                    }
                }

                if (requestId > 0 && Convert.ToInt32(copy) == 0 && UserType == (int)WFJ.Service.Model.UserType.ClientUser)
                {
                    _requestsService.UpdateRequestLastViewed(requestId.Value);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/AddPlacement?formId=" + formId + "&requestId=" + requestId, CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new AddEditPlacementsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }

        [HttpPost]
        public ActionResult SavePlacement(SavePlacementViewModel savePlacementsViewModel)
        {
            bool isSuccess = false;
            int requestId = 0;
            try
            {
                requestId = _formService.SavePlacements(savePlacementsViewModel);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/SavePlacement", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = isSuccess, requestId = requestId }, JsonRequestBehavior.AllowGet);
        }


        public void GetSessionUser(out int userId, out int userType, out int? userAccess)
        {
            if (Session["UserId"] != null)
            {
                userId = Convert.ToInt32(Session["UserId"].ToString());
                userType = Convert.ToInt32(Session["UserType"].ToString());
                userAccess = Session["UserAccess"] != null ? Convert.ToInt32(Session["UserAccess"].ToString()) : (int?)null;
            }
            else
            {
                userId = 0;
                userType = 0;
                userAccess = 0;
            }
        }

        public ActionResult GetStatusLongDescription(int statusCode, int formId)
        {
            IStatusCodesService _statusCodesService = new StatusCodesService();
            bool isSuccess = false;
            var descriptionLong = string.Empty;
            try
            {
                descriptionLong = _statusCodesService.GetByStatusCodeAndFormId(statusCode, formId).DescriptionLong;
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/GetStatusLongDescription", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = isSuccess, description = descriptionLong }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetRequestList(DataTablesParam param, string sortDir, string sortCol, bool isFirstTime,
                                           string beginDate, string endDate, int region, bool archived,
                                           int formId,
                                           int requestor = -1, int assignedAttorney = -1, int collector = -1,
                                           int statusCode = -1)
        {
            try
            {
                if (sortCol.Contains("."))
                {
                    sortCol = sortCol.Split('.')[1];
                }

                GetSessionUser(out UserId, out UserType, out UserAccess);

                PlacementRequestsListViewModel model = new PlacementRequestsListViewModel();
                int pageNo = 1;
                if (param.iDisplayStart >= param.iDisplayLength)
                    pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;


                model = _requestsService.GetPlacementRequests(UserId, formId, (UserType)((byte)UserType), requestor, assignedAttorney, collector, statusCode, region, beginDate, endDate, archived, param, sortDir, sortCol, pageNo);


                return Json(new
                {
                    aaData = model.Requests,
                    param.sEcho,
                    iTotalDisplayRecords = model.TotalRequestsCount,
                    iTotalRecords = model.TotalRequestsCount,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/GetPlacementList", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }

        [HttpPost]
        public ActionResult UpdateUserColumns(List<int> fieldIDs, int formId)
        {
            bool isSuccess = false;
            List<DatatableDynamicColumn> visibleColumns = new List<DatatableDynamicColumn>();
            List<DatatableDynamicColumn> allColumns = new List<DatatableDynamicColumn>();

            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                _requestsService.UpdateListFields(UserId, formId, fieldIDs);

                visibleColumns = _requestsService.GetDatatableColumns(UserId, formId, (UserType)((byte)UserType));
                allColumns = _requestsService.GetAllcolumns(UserId, formId, (UserType)((byte)UserType));

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/UpdateUserColumns", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = isSuccess, visibleColumns = visibleColumns, allColumns = allColumns });
        }

        [HttpPost]
        public ActionResult UpdateColumnSequence(List<DatatableDynamicColumn> fieldIDs, int formId)
        {
            bool isSuccess = false;
            List<DatatableDynamicColumn> visibleColumns = new List<DatatableDynamicColumn>();

            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                _requestsService.UpdateListFieldSequence(UserId, formId, fieldIDs);
                visibleColumns = _requestsService.GetDatatableColumns(UserId, formId, (UserType)((byte)UserType));
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/UpdateUserColumns", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess, visibleColumns = visibleColumns });
        }


        public ActionResult UpdateActiveRequest(int code, int requestId)
        {
            IStatusCodesService _statusCodesService = new StatusCodesService();
            bool isSuccess = false;
            try
            {
                isSuccess = true;
                _requestsService.UpdateActiveCode(code, requestId);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/UpdateActiveInactiveRequest", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = isSuccess }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddRequestDocument()
        {
            string message = string.Empty;
            bool isSuccess = false;
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    var requestId = Convert.ToInt32(Request.Form[0]);
                    var documentTypeId = Convert.ToInt32(Request.Form[1]);
                    var sendNote = Convert.ToBoolean(Request.Form[2]);

                    HttpPostedFileBase file = files[0];
                    string fname;

                    // Checking for Internet Explorer  
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                    }

                    /// check extension
                    string fileExtension = Path.GetExtension(fname).ToLower().Trim().Replace(".", "");
                    if (Util.ExtensionList().Contains(fileExtension))
                    {
                        // Get the complete folder path and store the file inside it.  
                        var path = Path.Combine(Server.MapPath("~/Uploads/"));
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        var fileName = Path.Combine(path, fname);
                        file.SaveAs(fileName);

                        /// save into database table
                        _requestDocumentService.Save(new RequestDocumentDetail()
                        {
                            DocumentTypeId = documentTypeId,
                            FileName = fname,
                            RequestId = requestId
                        });
                        message = "File Uploaded Successfully!";
                        isSuccess = true;
                    }
                    else
                    {
                        message = "Format is not supported";
                    }
                    /// send mail to assigned attorney and requestor
                    if (sendNote && isSuccess)
                    {
                        var request = _requestsService.GetByRequestId(requestId);
                        _formService.sendDocumentMail(request.AssignedAttorneyEmail,request.RequestorEmail, Models.Enums.RequestDocumentType.add.ToString());
                    }

                    string requestDocumentHtml = string.Empty;
                    if (isSuccess)
                    {
                        requestDocumentHtml = GetRequestDocumentGridHtml(requestId);
                    }
                    return Json(new { success = isSuccess, html = requestDocumentHtml, message= message }, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    _errorLogService.Add(new ErrorLogModel() { Page = "Placements/AddRequestDocument", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                    return Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { success = false,html= "No files selected." }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult RemoveRequestDocument(int requestDocumentId,int requestId,string fileName,bool sendNotice)
        {
            string message = string.Empty;
            bool success = false;
            string requestDocumentHtml = string.Empty;
            try
            {
                _requestDocumentService.Delete(requestDocumentId);
                var path = Path.Combine(Server.MapPath("~/Uploads/"));
                var fName = Path.Combine(path, fileName);
                System.IO.File.Delete(fName);
                message = "Deleted Successfully";
                requestDocumentHtml = GetRequestDocumentGridHtml(requestId);
                /// send mail to assigned attorney and requestor
                if (sendNotice)
                {
                    var request = _requestsService.GetByRequestId(requestId);
                    _formService.sendDocumentMail(request.AssignedAttorneyEmail, request.RequestorEmail, Models.Enums.RequestDocumentType.remove.ToString());
                }
                success = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/RemoveRequestDocument", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = success, html = requestDocumentHtml }, JsonRequestBehavior.AllowGet);
        }

        public string GetRequestDocumentGridHtml(int requestId)
        {
            RequestDocumentViewModel requestDocumentViewModels = new RequestDocumentViewModel();
            requestDocumentViewModels.RequestDocumentDetails = _requestDocumentService.GetbyRequestId(requestId);
            requestDocumentViewModels.DocumentType = _codeService.GetAllByType("DOCTYPE");
            return this.RenderPartialViewToString("_requestDocumentGrid", requestDocumentViewModels);
        }
    }
}