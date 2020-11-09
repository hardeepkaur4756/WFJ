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
    //[Authorize]
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

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                GetSessionUser(out UserId, out UserType);

                PlacementsViewModel model = new PlacementsViewModel();
                model.placementsFilterViewModel = new PlacementsFilterViewModel()
                {
                    client = UserType == (int)Web.Models.Enums.UserType.ClientUser ? _userClientService.GetUserClients(UserId,1) : _clientService.GetActiveInactiveOrderedList(),
                    placementTypeModels = _formTypeService.GetAll(),
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
                GetSessionUser(out UserId, out UserType);

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

        public ActionResult ViewPlacements(int id) // client name in header is pending
        {
            try
            {
                GetSessionUser(out UserId, out UserType);

                IUserService _userService = new UserService();
                IStatusCodesService _statusCodesService = new StatusCodesService();
                PlacementReuestsViewModel model = new PlacementReuestsViewModel();
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
                GetSessionUser(out UserId, out UserType);

                IStatusCodesService _statusCodesService = new StatusCodesService();
                AddEditPlacementsViewModel model = new AddEditPlacementsViewModel
                {
                    FormSections = _formService.GetFormSections(),
                    FormFieldsList = _formService.GetFormFieldsByForm(formId),
                    Collectors= _formService.GetCollectorsDropdown(),
                    Requestors= _formService.GetRequestorsDropdown(formId),
                    StatusList= _statusCodesService.GetByFormID(formId)
                };

                if(requestId == null)
                {
                    model.Request = new RequestViewModel();
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
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/AddPlacement", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new AddEditPlacementsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }
        }



        public void GetSessionUser(out int userId, out int userType)
        {
            if (Session["UserId"] != null)
            {
                userId = Convert.ToInt32(Session["UserId"].ToString());
                userType = Convert.ToInt32(Session["UserType"].ToString());
            }
            else
            {
                userId = 0;
                userType = 0;
            }
        }
 
    }
}