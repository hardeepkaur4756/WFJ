using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class LocalCounselModel
    {
        public int Id { get; set; }
        public string FirmName { get; set; }
        public string ContactName { get; set; }
        public string AttorneyName { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int DoNotUse { get; set; }
    }
}
