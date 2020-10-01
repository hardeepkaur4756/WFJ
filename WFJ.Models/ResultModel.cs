using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ResultModel
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public bool IsPasswordExpire { get; set; }
    }
}
