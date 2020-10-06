using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class PracticeAreaModel
    {
        public int ID { get; set; }
        public string PracticeAreaName { get; set; }
        public string Description { get; set; }
        public Nullable<byte> Status { get; set; }
    }
}
