using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace WFJ.Models
{
    public class PlacementReuestsViewModel : ExceptionModel
    {
        public string ClientName { get; set; }
        public PlacementReuestsFilterViewModel placementReuestsFilterViewModel { get; set; }
    }

    public class PlacementReuestsFilterViewModel
    {
        public List<SelectListItem> Requestors { get; set; }
        public List<SelectListItem> AssignedToList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> RegionList { get; set; }
        public List<SelectListItem> Collectors { get; set; }
    }
}
