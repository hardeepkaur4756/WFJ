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
        public string FormType { get; set; }
        public string ClientName { get; set; }

        public PlacementReuestsFilterViewModel placementReuestsFilterViewModel { get; set; }

        public IEnumerable<DatatableDynamicColumn> TableColumns { get; set; }
        public IEnumerable<DatatableDynamicColumn> AllColumnsList { get; set; }
    }

    public class PlacementReuestsFilterViewModel
    {
        public int FormID { get; set; }
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
        public Nullable<int> RequestorID { get; set; }
        public string Requestor { get; set; }
        public Nullable<int> AssignedAttorneyID { get; set; }
        public string AssignedAttorney { get; set; }
        public Nullable<int> AssignedCollectorIDVal { get; set; }
        public string AssignedCollectorID { get; set; }
        public Nullable<System.DateTime> RequestDateVal { get; set; }
        public string RequestDate { get; set; }
        public Nullable<int> StatusCodeID { get; set; }
        public string StatusCode { get; set; }
        public Nullable<System.DateTime> CompletionDateVal { get; set; }
        public string CompletionDate { get; set; }
        public Nullable<System.DateTime> LastViewedVal { get; set; }
        public string LastViewed { get; set; }
        public Nullable<System.DateTime> LastNoteDateVal { get; set; }
        public string LastNoteDate { get; set; }
        //public Nullable<double> TotalPayments { get; set; }
        public float TotalPayments { get; set; }
        public int DaysOpen { get; set; }

        public Dictionary<string, string> FormFields { get; set; }

    }


    //public class 



}
