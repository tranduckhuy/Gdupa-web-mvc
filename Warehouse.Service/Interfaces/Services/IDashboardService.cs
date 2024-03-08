using Warehouse.Shared.DTOs;

namespace Warehouse.Service.Interfaces.Services
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardInfoAsync(int year);
    }
}
