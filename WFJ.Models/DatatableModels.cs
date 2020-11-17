using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class DatatableDynamicColumn
    {
        public string data { get; set; }
        public string title { get; set; }
        public bool visible { get; set; }
        public int? seqNo { get; set; }
        public int fieldID { get; set; }
        public string defaultContent { get; set; }
    }
}
