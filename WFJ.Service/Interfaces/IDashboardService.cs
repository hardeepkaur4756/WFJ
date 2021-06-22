using WFJ.Models;

namespace WFJ.Service.Interfaces
{
    public interface IDashboardService
    {
        AdminDashboardViewModel GetAdminDashboardData();
        UserDashboardViewModel GetUserDashboardData(int userId);

    }
}
