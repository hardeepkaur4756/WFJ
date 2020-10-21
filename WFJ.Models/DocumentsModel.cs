using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class DocumentsModel
    {
        public int ID { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string StateCode { get; set; }
        public string DocumentType { get; set; }
        public string ProjectType { get; set; }
        public string FormType { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> PracticeAreaID { get; set; }
        public string WFJFileNbr { get; set; }
        public Nullable<int> Days { get; set; }
        public string Description { get; set; }
        public Nullable<int> CategoryID { get; set; }
        public Nullable<int> EmployeeCategoryID { get; set; }
        public string StateCodeID { get; set; }
        public string DocumentTypeID { get; set; }
        public string ProjectTypeID { get; set; }
        public string FormTypeID { get; set; }
        public Nullable<int> SeqNo { get; set; }
        public string ClientName { get; set; }
        public string PracticeAreaName { get; set; }
        public int? CurrentUserType { get; set; }
        public string State { get; set; }
    }
}
