using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.Services
{
    public interface IDashboardService
    {
		Task<DashboardDTO> GetDashboardInfoAsync(int year);
		DashboardDTO GetDashboardInfo(int year);
	}
}
