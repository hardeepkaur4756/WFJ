using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class ManageDocumentModel
    {
        public int? totalUsersCount { get; set; }
        public List<DocumentsModel> documents { get; set; }
    }
}
