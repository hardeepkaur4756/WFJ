using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class FormTypeModel
    {
        public int FormTypeID { get; set; }
        public string FormType1 { get; set; }
        public Nullable<byte> displayInSearch { get; set; }
        public Nullable<int> sequenceID { get; set; }
    }
}
