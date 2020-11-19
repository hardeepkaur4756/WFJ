using System;
using System.Collections.Generic;
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
        //private ICategoryService _categoryService = new CategoryService();
        //private IPracticeAreaService _practiceAreaService = new PracticeAreaService();
        private IFormTypeService _formTypeService = new FormTypeService();
        private IUserService _userService = new UserService();
        private IRequestsService _requestsService = new RequestsService();

        private IUserClientService _userClientService = new UserClientService();

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
                    client = UserType == (int)Web.Models.Enums.UserType.ClientUser ? _userClientService.GetUserClients(UserId,1) : _clientService.GetActiveInactiveOrderedList(),
                    placementTypeModels = _formTypeService.GetAll().Where(x => x.FormType1 != null).ToList(),
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
                    model = _formService.GetPlacements(clientId, formTypeId, searchKeyword, param, sortDir, sortCol, pageNo, userSpecific);
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

        public ActionResult ViewPlacements(int id)
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                IUserService _userService = new UserService();
                IStatusCodesService _statusCodesService = new StatusCodesService();
                PlacementReuestsViewModel model = new PlacementReuestsViewModel();

                var form = _formService.GetFormById(id);
                model.ClientName = form.ClientName;//form.Client != null ? form.Client.ClientName : null;
                model.FormType = form.FormTypeName;
                model.FormID = id;
                model.TableColumns = _requestsService.GetDatatableColumns(UserId, id, (UserType)((byte)UserType));
                model.AllColumnsList = _requestsService.GetAllcolumns(UserId, id, (UserType)((byte)UserType));

                model.placementReuestsFilterViewModel = new PlacementReuestsFilterViewModel()
                {
                    FormID = id,
                    Requestors = _formService.GetRequestorsDropdown(id),
                    RegionList = _clientService.GetRegionsDropdown(),
                    Collectors = _formService.GetCollectorsDropdown(),
                    StatusList = _statusCodesService.GetByFormID(id),
                    AssignedToList = _formService.GetPersonnelsDropdown(id)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/ViewPlacements", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new PlacementReuestsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }


        public ActionResult AddPlacement(int formId, int? requestId)
        {
            try
            {
                string requestorName = string.Empty;
                GetSessionUser(out UserId, out UserType, out UserAccess);
                var form = _formService.GetFormById(formId);

                var user = _userService.GetById(UserId);
                int clientId = Convert.ToInt32(user.ClientID);
                if (clientId > 0)
                {
                    requestorName = _clientService.GetRequestorNameById(clientId);
                }
                requestorName = string.IsNullOrEmpty(requestorName) ? "Requestor" : requestorName;
                IStatusCodesService _statusCodesService = new StatusCodesService();
                ICurrenciesService _currenciesService = new CurrenciesService();
                AddEditPlacementsViewModel model = new AddEditPlacementsViewModel
                {
                    ClientName = form.Client != null ? form.Client.ClientName : null,
                    CurrencyDropdown = _currenciesService.GetCurrencyDropdown(),
                    FormSections = _formService.GetFormSections(),
                    FormFieldsList = _formService.GetFormFieldsByForm(formId, requestId),
                    Collectors = _formService.GetCollectorsDropdown(),
                    Requestors = _formService.GetRequestorsDropdown(formId),
                    StatusList = _statusCodesService.GetByFormID(formId),
                    AssignedAtorneys = _formService.GetPersonnelsDropdown(formId),
                    UserAccess = UserAccess,
                    UserType = UserType,
                    ClientId = Convert.ToInt32(form.ClientID),
                    isEditMode = Convert.ToInt32(requestId) > 0 ? true : false,
                    RequestorName = requestorName
                };

                if(requestId == null)
                {
                    model.Request = new RequestViewModel { FormID = formId };
                }
                else
                {
                    IRequestsService _requestService = new RequestsService();
                    model.Request = _requestService.GetByRequestId(requestId.Value);
                }

                if(requestId > 0 && UserType == (int)WFJ.Service.Model.UserType.ClientUser)
                {
                    _requestsService.UpdateRequestLastViewed(requestId.Value);
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/AddPlacement?formId="+ formId+"&requestId="+requestId, CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
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


        public void GetSessionUser(out int userId, out int userType,out int? userAccess)
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

        public ActionResult GetStatusLongDescription(int statusCode,int formId)
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
                                           string beginDate, string endDate, string region, bool archived,
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


                //int? userSpecific = UserType == (int)Web.Models.Enums.UserType.ClientUser ? UserId : (Nullable<int>)null;

                //if ((int)WFJ.Web.Models.Enums.UserType.SystemAdministrator != UserType || isFirstTime == false)
                model = _requestsService.GetPlacementRequests(UserId, formId, (UserType)((byte)UserType), requestor, assignedAttorney, collector, statusCode, region, beginDate, endDate, archived, param, sortDir, sortCol, pageNo);
                //else
                //    model.placements = new List<PlacementsModel>();

                Dictionary<string, object> Fields = new Dictionary<string, object>();
                Fields.Add("test1", new { test = 1 });

                return Json(new
                {
                    aaData = model.Requests,
                    testData = Fields,
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
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                _requestsService.UpdateListFields(UserId, formId, fieldIDs);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/UpdateUserColumns", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = isSuccess });
        }
        [HttpPost]
        public ActionResult UpdateColumnSequence(List<DatatableDynamicColumn> fieldIDs, int formId)
        {
            bool isSuccess = false;
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);
                _requestsService.UpdateListFieldSequence(UserId, formId, fieldIDs);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/UpdateUserColumns", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = isSuccess });
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
            return Json(new { success = isSuccess}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CopyPlacement(int formId, int requestId)
        {
            try
            {
                IRequestsService _requestService = new RequestsService();
                string requestorName = string.Empty;
                GetSessionUser(out UserId, out UserType, out UserAccess);
                var form = _formService.GetFormById(formId);
                var user = _userService.GetById(UserId);
                int clientId = Convert.ToInt32(user.ClientID);
                if (clientId > 0)
                {
                    requestorName = _clientService.GetRequestorNameById(clientId);
                }
                requestorName = string.IsNullOrEmpty(requestorName) ? "Requestor" : requestorName;
                IStatusCodesService _statusCodesService = new StatusCodesService();
                ICurrenciesService _currenciesService = new CurrenciesService();
                AddEditPlacementsViewModel model = new AddEditPlacementsViewModel
                {
                    ClientName = form.Client != null ? form.Client.ClientName : null,
                    CurrencyDropdown = _currenciesService.GetCurrencyDropdown(),
                    FormSections = _formService.GetFormSections(),
                    FormFieldsList = _formService.GetFormFieldsByForm(formId, requestId),
                    Collectors = _formService.GetCollectorsDropdown(),
                    Requestors = _formService.GetRequestorsDropdown(formId),
                    StatusList = _statusCodesService.GetByFormID(formId),
                    AssignedAtorneys = _formService.GetPersonnelsDropdown(formId),
                    UserAccess = UserAccess,
                    UserType = UserType,
                    ClientId = Convert.ToInt32(form.ClientID),
                    isEditMode = false,
                    RequestorName = requestorName
                };
                model.Request = _requestService.GetByRequestId(requestId);
                model.Request.ID = 0;
                return View("AddPlacement", model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/CopyPlacement?formId=" + formId + "&requestId=" + requestId, CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new AddEditPlacementsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }

    }
}