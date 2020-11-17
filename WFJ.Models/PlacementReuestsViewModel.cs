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
        public int FormID { get; set; }
        public string ClientName { get; set; }
        public PlacementReuestsFilterViewModel placementReuestsFilterViewModel { get; set; }

        public IEnumerable<DatatableDynamicColumn> TableColumns { get; set; }
    }

    public class PlacementReuestsFilterViewModel
    {
        public List<SelectListItem> Requestors { get; set; }
        public List<SelectListItem> AssignedToList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
        public List<SelectListItem> RegionList { get; set; }
        public List<SelectListItem> Collectors { get; set; }
    }

    public class PlacementRequestsListViewModel
    {
        public int? TotalRequestsCount { get; set; }
        public List<PlacementRequestModel> Requests { get; set; }
    }

    public class PlacementRequestModel
    {
        public int RequestID { get; set; }
        public Nullable<int> Requestor { get; set; }
        public string RequestorName { get; set; }
        public Nullable<int> AssignedAttorney { get; set; }
        public string AssignedAttorneyName { get; set; }
        public Nullable<int> AssignedCollectorID { get; set; }
        public string CollectorName { get; set; }
        public Nullable<System.DateTime> RequestDate { get; set; }
        public string RequestDateString { get; set; }
        public Nullable<int> StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public Nullable<System.DateTime> CompletionDate { get; set; }
        public string CompletionDateString { get; set; }
        public Nullable<System.DateTime> LastViewed { get; set; }
        public string LastViewedDateString { get; set; }
        //public Nullable<System.DateTime> LastNoteDate { get; set; }
        //public Nullable<double> TotalPayments { get; set; }
        public float TotalPaymentsAmount { get; set; }
        public int DaysOpen { get; set; }

        public Dictionary<string, string> FormFields { get; set; }

    }


    //public class 



}
