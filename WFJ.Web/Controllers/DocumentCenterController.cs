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
        // GET: DocumentCenter
        [HttpGet]
        public ActionResult GetList()
        {
            try
            {
                ManageDocumentViewModel manageDocumentViewModel = new ManageDocumentViewModel();
                manageDocumentViewModel.ManageDocumentFilterViewModel = new ManageDocumentFilterViewModel()
                {
                    clientModels = _clientService.GetClients(),
                    practiceAreaModels = _practiceAreaService.GetAll(),
                    categoryModels = _categoryService.GetAll(),
                    formTypeModels = _formTypeService.GetAll()
                };

                return View(manageDocumentViewModel);
            }
            catch (Exception ex)
            {
                _errorLogService.Add(new ErrorLogModel() { Page = "DocumentCenter/GetList", CreatedBy = Convert.ToInt32(Session["UserId"]), CreateDate = DateTime.Now, ErrorText = ex.ToMessageAndCompleteStacktrace() });
                return View(new ManageDocumentViewModel() { ErrorMessage = "Sorry, An error occurred!" });
                
            }
            
        }

        [HttpGet]
        public JsonResult GetDocumentList(DataTablesParam param, string sortDir, string sortCol, int clientId = -1, int documentTypeId = -1, int projectTypeId=-1, int practiceAreaId=-1,int categoryId=-1,int formTypeId=-1, string searchKeyword = "")
        {
            try
            {
                ManageDocumentModel model = new ManageDocumentModel();
                int pageNo = 1;
                if (param.iDisplayStart >= param.iDisplayLength)
                    pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;

                model = _documentSearchService.GetDocuments(clientId, documentTypeId, projectTypeId, practiceAreaId, categoryId, formTypeId, searchKeyword, param, sortDir, sortCol, pageNo);
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
    }
}