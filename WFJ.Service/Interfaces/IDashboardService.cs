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
        ChartBaseModelYearly GetPlacementsLineChartData(int formId);
        ChartBaseModelYearly GetDollarsPlacedLineChartData(int formId);
        ChartBaseModelYearly GetPlacementCollectedLineChartData(int formId);
        ChartBaseModelYearly GetDollarsCollectedLineChartData(int formId);



    }
}
