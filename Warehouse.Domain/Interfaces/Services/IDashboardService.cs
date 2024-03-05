using Warehouse.Domain.DTOs;

namespace Warehouse.Domain.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardDTO> GetDashboardInfoAsync(int year);
    }
}
