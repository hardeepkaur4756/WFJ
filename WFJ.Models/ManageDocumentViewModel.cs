using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WFJ.Models
{
   public class ManageDocumentViewModel:ExceptionModel
    {
        public ManageDocumentFilterViewModel ManageDocumentFilterViewModel { get; set; }
    }
    public class ManageDocumentFilterViewModel
    {
        //public List<ClientModel> clientModels { get; set; }
        public List<SelectListItem> client { get; set; }
        //public List<PracticeAreaModel> practiceAreaModels { get; set; }
        public List<SelectListItem> practiceArea { get; set; }
        public List<CategoryModel> categoryModels { get; set; }
        public List<FormTypeModel> formTypeModels { get; set; }
        public List<SelectListItem> documentType { get; set; }
        public DocumentViewModel documentViewModel { get; set; }

        public List<SelectListItem> state { get; set; }
        public HttpPostedFileBase documentFile { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
    }
    public class ManageDocumentDataViewModel
    {
        //public List<UserModel> Users { get; set; }
    }
}
