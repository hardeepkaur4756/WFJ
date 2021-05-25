using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class PersonnelRequestModel
    {
        public int ID { get; set; }
        public Nullable<int> RequestID { get; set; }
        public Nullable<int> FirmID { get; set; }
        public Nullable<int> localCounselStatus { get; set; }
        public Nullable<double> localCounselRate { get; set; }
    }
}
