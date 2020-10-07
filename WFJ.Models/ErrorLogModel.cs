using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class ErrorLogModel
    {
        public int ID { get; set; }
        public string ErrorText { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<int> CreatedBy { get; set; }
        public string Page { get; set; }
    }
}
