using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.Services
{
    public interface IDashboardService
    {
        DashboardDTO GetDashboardInfo(int year);
    }
}
