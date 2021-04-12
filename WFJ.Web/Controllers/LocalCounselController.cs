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
        public JsonResult GetLocalCounselList(DataTablesParam param, string sortDir, string sortCol, bool isFirstTime, string firmName, string attorneyName, string city, int stateId, int countryId)
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

                if (isFirstTime == false)
                {
                    model = _localCounselService.GetLocalCounsels(firmName,attorneyName, city, state, country, param, sortDir, sortCol, pageNo);
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
    }
}