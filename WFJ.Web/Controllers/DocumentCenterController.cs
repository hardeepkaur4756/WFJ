using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WFJ.Service;
using WFJ.Service.Interfaces;
using WFJ.Models;
namespace WFJ.Web.Controllers
{
    public class DocumentCenterController : Controller
    {
        private IDocumentSearchService _documentSearchService = new DocumentSearchService();
        private IClientService _clientService = new ClientService();
        private ICategoryService _categoryService = new CategoryService();
        private IPracticeAreaService _practiceAreaService = new PracticeAreaService();
        private IFormTypeService _formTypeService = new FormTypeService();
        // GET: DocumentCenter
        [HttpGet]
        public ActionResult GetList()
        {
            ManageDocumentViewModel manageDocumentViewModel = new ManageDocumentViewModel();
            manageDocumentViewModel.ManageDocumentFilterViewModel = new ManageDocumentFilterViewModel()
            {
                clientModels = _clientService.GetClients(),
                practiceAreaModels= _practiceAreaService.GetAll(),
                categoryModels= _categoryService.GetAll(),
                formTypeModels= _formTypeService.GetAll()
            };

            return View(manageDocumentViewModel);
        }

        [HttpGet]
        public JsonResult GetDocumentList(DataTablesParam param, string sortDir, string sortCol, int clientId = -1, int documentTypeId = -1, int projectTypeId=-1, int practiceAreaId=-1,int categoryId=-1,int formTypeId=-1, string searchKeyword = "")
        {
            ManageDocumentModel model = new ManageDocumentModel();
            int pageNo = 1;
            if (param.iDisplayStart >= param.iDisplayLength)
                pageNo = (param.iDisplayStart / param.iDisplayLength) + 1;

            model = _documentSearchService.GetDocuments(clientId, documentTypeId,projectTypeId,practiceAreaId,categoryId,formTypeId,searchKeyword, param,sortDir, sortCol,pageNo);
            return Json(new
            {
                aaData = model.documents,
                param.sEcho,
                iTotalDisplayRecords = model.totalUsersCount,
                iTotalRecords = model.totalUsersCount
            }, JsonRequestBehavior.AllowGet);
        }
    }
}