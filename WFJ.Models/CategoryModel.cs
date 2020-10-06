using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
   public class CategoryModel
    {
        public int CategoryID { get; set; }
        public Nullable<int> PracticeAreaID { get; set; }
        public string CategoryName { get; set; }
        public string PracticeAreaName { get; set; }
    }
}
