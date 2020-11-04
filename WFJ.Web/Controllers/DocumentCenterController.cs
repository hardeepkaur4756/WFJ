using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Models;
using WFJ.Helper;

namespace WFJ.Web.Controllers
{
    public class DocumentCenterController : Controller
    {
        private IErrorLogService _errorLogService = new ErrorLogService();
        private IDocumentSearchService _documentSearchService = new DocumentSearchService();
        private IClientService _clientService = new ClientService();
        private ICategoryService _categoryService = new CategoryService();
        private IPracticeAreaService _practiceAreaService = new PracticeAreaService();
        private IFormTypeService _formTypeService = new FormTypeService();
        private ICodesService _codesService = new CodesService();
        private IUserClientService _userClientService = new UserClientService();
        // GET: DocumentCenter
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
               
                ManageDocumentViewModel manageDocumentViewModel = new ManageDocumentViewModel();
                manageDocumentViewModel.ManageDocumentFilterViewModel = new ManageDocumentFilterViewModel()
                {
                    documentViewModel = new DocumentViewModel(),
                    client = userType == (int)Web.Models.Enums.UserType.ClientUser ? _userClientService.GetUserClients(UserId) : _clientService.GetAllClients(),
                    practiceArea = _practiceAreaService.GetAllPracticeArea(),
                    categoryModels = _categoryService.GetAll(),
                    formTypeModels = _formTypeService.GetAll(),
                    documentType = _codesService.GetAllByType("DOCTYPE"),
                    state = _codesService.GetAllStateByType("STATE")
                };

                return View(manageDocumentViewModel);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "DocumentCenter/Index", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new ManageDocumentViewModel() { ErrorMessage = "Sorry, An error occurred!" });

            }

        }

        [HttpGet]
        public JsonResult GetDocumentList(DataTablesParam param, string sortDir, string sortCol, int clientId = -1, int documentTypeId = -1, int practiceAreaId=-1,int categoryId=-1,int formTypeId=-1, string searchKeyword = "")
        {
            try
            {
                ManageDocumentModel model = new ManageDocumentModel();
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

                model = _documentSearchService.GetDocuments(clientId, documentTypeId, practiceAreaId, categoryId, formTypeId, searchKeyword, param, sortDir, sortCol, pageNo, userSpecific);
                return Json(new
                {
                    aaData = model.documents,
                    param.sEcho,
                    iTotalDisplayRecords = model.totalUsersCount,
                    iTotalRecords = model.totalUsersCount,
                    Success = true
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "DocumentCenter/GetDocumentList", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });

            }
        }


        [HttpGet]
        public ActionResult AddDocument()
        {
            try
            {
                ManageDocumentFilterViewModel manageDocumentFilterViewModel = new ManageDocumentFilterViewModel();
                manageDocumentFilterViewModel.documentViewModel = new DocumentViewModel();
                manageDocumentFilterViewModel.client = _clientService.GetAllClients();
                /*manageDocumentFilterViewModel.practiceAreaModels = _practiceAreaService.GetAll();*/
                manageDocumentFilterViewModel.practiceArea = _practiceAreaService.GetAllPracticeArea();
                manageDocumentFilterViewModel.categoryModels = _categoryService.GetAll();
                manageDocumentFilterViewModel.formTypeModels = _formTypeService.GetAll();
                manageDocumentFilterViewModel.documentType = _codesService.GetAllByType("DOCTYPE");
                manageDocumentFilterViewModel.state = _codesService.GetAllStateByType("STATE");
                return Json(new { Success = true, Html = this.RenderPartialViewToString("_addEditDocument", manageDocumentFilterViewModel) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "DocumentCenter/AddDocument", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }

        [HttpPost]

        public ActionResult AddDocument(ManageDocumentFilterViewModel manageDocumentFilterViewModel)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _documentSearchService.AddOrUpdate(manageDocumentFilterViewModel);
                return Json(new { Success = manageDocumentFilterViewModel.IsSuccess, Message = manageDocumentFilterViewModel.Message }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                List<string> ErrorMessages = new List<string>();
                var errors = ModelState.Select(x => x.Value.Errors)
                           .Where(y => y.Count > 0)
                           .ToList();
                if (errors != null)
                {
                     ErrorMessages  = errors.SelectMany(x=>x.Select(y=>y.ErrorMessage)).ToList();
                }
                return Json(new { Success = false,ErrorMessages=ErrorMessages }, JsonRequestBehavior.AllowGet);
            }
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "DocumentCenter/AddDocument", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });

            }

            //return View();
        }

        [HttpGet]
        public ActionResult EditDocument(int id)
        {
            try
            {
                ManageDocumentFilterViewModel manageDocumentFilterViewModel = id > 0 ? _documentSearchService.GetDocumentById(id) : new ManageDocumentFilterViewModel();
            manageDocumentFilterViewModel.documentType = _codesService.GetAllByType("DOCTYPE");
            manageDocumentFilterViewModel.state = _codesService.GetAllStateByType("STATE");
            
            manageDocumentFilterViewModel.practiceArea = _practiceAreaService.GetAllPracticeArea();
            return Json(new { Success = true, Html = this.RenderPartialViewToString("_addEditDocument", manageDocumentFilterViewModel), ClientId = manageDocumentFilterViewModel.documentViewModel.ClientId }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "DocumentCenter/EditDocument", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return Json(new { Message = "Sorry, An error occurred!", Success = false });
            }
        }
        
    }
}