using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class ManageDocumentViewModel
    {
        public ManageDocumentFilterViewModel ManageDocumentFilterViewModel { get; set; }
    }
    public class ManageDocumentFilterViewModel
    {
        public List<ClientModel> clientModels { get; set; }
        public List<PracticeAreaModel> practiceAreaModels { get; set; }
        public List<CategoryModel> categoryModels { get; set; }
        public List<FormTypeModel> formTypeModels { get; set; }
    }
    public class ManageDocumentDataViewModel
    {
        //public List<UserModel> Users { get; set; }
    }
}
