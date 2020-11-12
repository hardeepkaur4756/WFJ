using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Helper;
using WFJ.Models;
using WFJ.Service;
using WFJ.Service.Interfaces;

namespace WFJ.Web.Controllers
{
    [CustomAttribute.AuthorizeActivity((int)Web.Models.Enums.UserType.None)]
    public class PlacementsController : Controller
    {
        private IErrorLogService _errorLogService = new ErrorLogService();
        private IFormService _formService = new FormService();
        private IClientService _clientService = new ClientService();
        //private ICategoryService _categoryService = new CategoryService();
        //private IPracticeAreaService _practiceAreaService = new PracticeAreaService();
        private IFormTypeService _formTypeService = new FormTypeService();
        //private ICodesService _codesService = new CodesService();

        private IUserClientService _userClientService = new UserClientService();

        private int UserType = 0;
        private int UserId = 0;
        private int UserAccess = 0;

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
                model.ClientName = form.Client != null ? form.Client.ClientName : null;

                model.placementReuestsFilterViewModel = new PlacementReuestsFilterViewModel()
                {
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
                GetSessionUser(out UserId, out UserType, out UserAccess);
                var form = _formService.GetFormById(formId);

                IStatusCodesService _statusCodesService = new StatusCodesService();
                ICurrenciesService _currenciesService = new CurrenciesService();
                AddEditPlacementsViewModel model = new AddEditPlacementsViewModel
                {
                    ClientName = form.Client != null ? form.Client.ClientName : null,
                    CurrencyDropdown = _currenciesService.GetCurrencyDropdown(),
                    FormSections = _formService.GetFormSections(),
                    FormFieldsList = _formService.GetFormFieldsByForm(formId),
                    Collectors = _formService.GetCollectorsDropdown(),
                    Requestors = _formService.GetRequestorsDropdown(formId),
                    StatusList = _statusCodesService.GetByFormID(formId),
                    AssignedAtorneys = _formService.GetPersonnelsDropdown(formId),
                    UserAccess = UserAccess,
                    UserType = UserType,
                    ClientId = Convert.ToInt32(form.ClientID),
                    isEditMode = false
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
                   

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/AddPlacement?formId="+ formId+"&requestId="+requestId, CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new AddEditPlacementsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }

        [HttpPost]
        public ActionResult AddPlacement(AddEditPlacementsViewModel model)
        {
            return View();
        }


        public void GetSessionUser(out int userId, out int userType,out int userAccess)
        {
            if (Session["UserId"] != null)
            {
                userId = Convert.ToInt32(Session["UserId"].ToString());
                userType = Convert.ToInt32(Session["UserType"].ToString());
                userAccess = Convert.ToInt32(Session["UserAccess"].ToString()); 
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
    }
}