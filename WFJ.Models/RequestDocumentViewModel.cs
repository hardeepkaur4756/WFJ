using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class RequestDocumentViewModel
    {
        public List<RequestDocumentDetail> RequestDocumentDetails { get; set; }
        public List<SelectListItem> DocumentType { get; set; }
    }
    public class RequestDocumentDetail
    {
        public int RequestDocumentId { get; set; }
        public int RequestId { get; set; }
        public int DocumentTypeId { get; set; }
        public string FileName { get; set; }
    }
}
