using System.Collections.Generic;
using System.Web.Mvc;
using WFJ.Models;
using WFJ.Repository.EntityModel;

namespace WFJ.Service.Interfaces
{
    public interface IDashboardService
    {
        AdminDashboardViewModel GetAdminDashboardData(int formId);
        UserDashboardViewModel GetUserDashboardData(int formId);
        (Form form, List<SelectListItem>) GetDashboardFilters(int userType, int userId);
        ClientDashboardViewModel GetClientDashboardData(int formId);
        List<ChartBaseModel> GetActiveStatusPieChartData(int formId);
        ChartBaseModel3Yearly GetPlacementData(int formId);
        ChartBaseModelYearly GetDollarsPlacedLineChartData(int formId);
        ChartBaseModel3Yearly GetPlacementCollectedData(int formId);
        ChartBaseModelYearly GetDollarsCollectedLineChartData(int formId);

        List<StackBarChartModel> GetPlacementAndCollectedBarChartData(int formId);



    }
}
