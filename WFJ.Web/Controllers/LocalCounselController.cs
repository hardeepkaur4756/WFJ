using System;
using System.Collections.Generic;
using System.Configuration;
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
    public class LocalCounselController : Controller
    {
        private IErrorLogService _errorLogService = new ErrorLogService();
        private ILocalCounselService _localCounselService = new LocalCounselService();
        public IClientService _clientService = new ClientService();
        private ICodesService _codesService = new CodesService();
        private IPersonnelRequestService _personnelRequestService = new PersonnelRequestService();

        private int UserType = 0;
        private int UserId = 0;
        private int? UserAccess;

        [HttpGet]
        public ActionResult Index()
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                LocalCounselViewModel model = new LocalCounselViewModel
                {
                    localCounselFilterViewModel = new LocalCounselFilterViewModel()
                    {
                        states = _codesService.GetAllStateByType("STATE"),
                        countries = _codesService.GetAllStateByType("COUNTRY")
                    }
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "LocalCounsel/Index", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new LocalCounselViewModel() { ErrorMessage = "Sorry, An error occurred!" });
            }

        }

        [HttpGet]
        public JsonResult GetLocalCounselList(DataTablesParam param, string sortDir, string sortCol, bool isFirstTime, string firmName, string attorneyName, string contactName, string city, int stateId, int countryId)
        {
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                ManageLocalCounselModel model = new ManageLocalCounselModel();
                int pageNo = 1;
                if (param.iDisplayStart >= param.iDisplayLength)
                {
                    pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;
                }
                string state = "", country = "";
                state = _codesService.GetById(stateId)?.Code1;
                country = _codesService.GetById(countryId)?.Code1;
                if (isFirstTime == false)
                {
                    model = _localCounselService.GetLocalCounsels(firmName, attorneyName, contactName, city, state, country, param, sortDir, sortCol, pageNo);
                }
                else
                {
                    model.localCounsels = new List<LocalCounselModel>();
                }

                return Json(new
                {
                    aaData = model.localCounsels,
                    param.sEcho,
                    iTotalDisplayRecords = model.totalLocalCounselsCount,
                    iTotalRecords = model.totalLocalCounselsCount,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "LocalConsel/GetLocalCounselList", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });

            }
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

        [HttpPost]
        public ActionResult AddLocalCounsel(AddLocalCounselViewModel addLocalCounselViewModel)
        {
            bool isSuccess = false;
            try
            {
                _localCounselService.SaveLocalCounsel(addLocalCounselViewModel);
                 isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "LocalCounsel/AddLocalCounsel", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }
            return Json(new { success = isSuccess, firmId = addLocalCounselViewModel.FirmId }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetLocalCounselDetail(int firmId)
        {
            AddLocalCounselViewModel addLocalCounselViewModel = _localCounselService.GetById(firmId);
            addLocalCounselViewModel.fileInformation = _localCounselService.GetFileInformation(firmId);
            addLocalCounselViewModel.FirmId = firmId;
            var result = PartialView("_addLocalCounsel", addLocalCounselViewModel);
            return result;
        }

        [HttpPost]
        public ActionResult DeleteAssociateCounsel(int firmId)
        {
            bool isSuccess = false;
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                if (UserType == (byte)WFJ.Service.Model.UserType.SystemAdministrator)
                    _localCounselService.DeleteAssociateCounse(firmId);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "LocalCounsel/DeleteAssociateCounsel", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }
        [HttpPost]
        public ActionResult AddPersonnelRequests(PersonnelRequestModel personnelRequestModel)
        {
            int firmId = Convert.ToInt32( personnelRequestModel.FirmID);
            _personnelRequestService.Add(personnelRequestModel);
            var associateCounsel = new AssociateCounselModel();
            associateCounsel = _localCounselService.GetByFirmId(firmId);
            return PartialView("_assignedFile", associateCounsel);
        }
        [HttpPost]
        public ActionResult DeletePersonnelRequests(int requestId)
        {
            bool isSuccess = false;
            try
            {
                GetSessionUser(out UserId, out UserType, out UserAccess);

                if (UserType == (byte)WFJ.Service.Model.UserType.SystemAdministrator)
                    _personnelRequestService.DeletePersonnelRequest(requestId);

                isSuccess = true;
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "LocalCounsel/DeleteAssociateCounsel", CreatedBy = UserId, CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
            }

            return Json(new { success = isSuccess });
        }
        

    }
}