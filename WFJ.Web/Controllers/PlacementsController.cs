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

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                int userType = 0;
                int UserId = 0;
                if (Session["UserId"] != null)
                {
                    UserId = Convert.ToInt32(Session["UserId"].ToString());
                    userType = Convert.ToInt32(Session["UserType"].ToString());
                }

                PlacementsViewModel model = new PlacementsViewModel();
                model.placementsFilterViewModel = new PlacementsFilterViewModel()
                {
                    client = userType == (int)Web.Models.Enums.UserType.ClientUser ? _userClientService.GetUserClients(UserId,1) : _clientService.GetActiveInactiveOrderedList(),
                    placementTypeModels = _formTypeService.GetAll(),
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/Index", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new PlacementsViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }

        }

        [HttpGet]
        public JsonResult GetPlacementList(DataTablesParam param, string sortDir, string sortCol, bool isFirstTime, int clientId = -1, int formTypeId = -1, string searchKeyword = "")
        {
            try
            {
                ManagePlacementsModel model = new ManagePlacementsModel();
                int pageNo = 1;
                if (param.iDisplayStart >= param.iDisplayLength)
                    pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;

                int userType = 0;
                int UserId = 0;
                if (Session["UserId"] != null)
                {
                    UserId = Convert.ToInt32(Session["UserId"].ToString());
                    userType = Convert.ToInt32(Session["UserType"].ToString());
                }

                int? userSpecific = userType == (int)Web.Models.Enums.UserType.ClientUser ? UserId : (Nullable<int>)null;

                if ((int)WFJ.Web.Models.Enums.UserType.SystemAdministrator != userType || isFirstTime == false)
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
                _errorLogService.Add(new ErrorLogModel() { Page = "Placements/Inex", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });

            }
        }

        public ActionResult ViewPlacements()
        {
            return View();
        }

    }
}