using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ClientDashboardViewModel
    {
        public List<RecentAccountActivityViewModel> RecentAccountView { get; set; }
        public List<RecentAccountActivityViewModel> RecentActivityView { get; set; }
        public List<ApprovedRecentPayementViewModel> ApprovedPayements { get; set; }
        public List<ChartBaseModel> GetActiveStatusPieChartData { get; set; }
        public List<ApprovedRecentPayementViewModel> RemittancePayements { get; set; }

    }

    public class ChartBaseModel
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }

    public class ChartBaseModelYearly
    {
        public List<ChartBaseModel> ChartBaseModelCurrentYear { get; set; }
        public List<ChartBaseModel> ChartBaseModelPreviousYear { get; set; }
    }

    public class ChartBaseModel3Yearly
    {
        public List<ChartBaseModel> ChartBaseModelCurrentYear { get; set; }
        public List<ChartBaseModel> ChartBaseModelPreviousYear { get; set; }
        public List<ChartBaseModel> ChartBaseModellast3rdYear { get; set; }
    }

    public class StackBarChartModel
    {
        public string[] backgroundColor { get; set; }
        public string fillColor { get; set; }
        public string[] data { get; set; }
        public string stack { get; set; }
    }


}
