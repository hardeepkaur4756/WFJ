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
        private IClientService _clientService = new ClientService();
        private ICategoryService _categoryService = new CategoryService();
        private IPracticeAreaService _practiceAreaService = new PracticeAreaService();
        private IFormTypeService _formTypeService = new FormTypeService();
        // GET: DocumentCenter
        public ActionResult DocumentCenter()
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
    }
}