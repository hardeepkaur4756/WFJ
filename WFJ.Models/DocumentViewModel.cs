using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public string StateCode { get; set; }
        public Nullable<int> StateCodeId { get; set; }
        public Nullable<int> DocumentTypeId { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Document Name.")]
        public string DocumentName { get; set; }
        public string Description { get; set; }
        //public List<string> ClientId { get; set; }
        public int[] ClientId { get; set; }
        public Nullable<int> PracticeAreaId { get; set; }
        public string FileName { get; set; }
       
    }
}
