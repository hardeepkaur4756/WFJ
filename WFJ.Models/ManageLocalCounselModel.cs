using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ManageLocalCounselModel
    {
        public int? totalLocalCounselsCount { get; set; }
        public List<LocalCounselModel> localCounsels { get; set; }
    }
}
