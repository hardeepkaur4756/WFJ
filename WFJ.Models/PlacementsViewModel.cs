using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class PlacementsFilterViewModel
    {
        public List<SelectListItem> client { get; set; }
        public List<FormTypeModel> placementTypeModels { get; set; }
    }
    public class PlacementsViewModel : ExceptionModel
    {
        public PlacementsFilterViewModel placementsFilterViewModel { get; set; }
    }
}
