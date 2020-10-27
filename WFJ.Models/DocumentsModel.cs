using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class DocumentsModel
    {
        public int Id { get; set; }
        public string DocumentName { get; set; }
        public string FileName { get; set; }
        public string StateCode { get; set; }
        public string DocumentType { get; set; }
        public string ProjectType { get; set; }
        public string FormType { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> PracticeAreaId { get; set; }
        public string WFJFileNbr { get; set; }
        public Nullable<int> Days { get; set; }
        public string Description { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> EmployeeCategoryId { get; set; }
        public Nullable<int> StateCodeId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        public string ProjectTypeId { get; set; }
        public string FormTypeId { get; set; }
        public Nullable<int> SeqNo { get; set; }
        public string ClientName { get; set; }
        public string PracticeAreaName { get; set; }
        public int? CurrentUserType { get; set; }
        public string State { get; set; }
        public string DocumentFullPath { get; set; }
    }
}
