using Warehouse.Shared.DTOs;
using Warehouse.Shared.ViewModels;

namespace Warehouse.Service.Interfaces.Services
{
    public interface IWarehouseService
    {
        Task<WarehouseViewModel> GetLimitAsync(int page, int quarter, int year);

        Task<WarehouseViewModel> GetByStatusAsync(string status);

        bool Add(ImportProductsDTO importProducts);

        Task<WarehouseImportViewModel> GetDataViewImportAsync();

        WarehouseViewModel SearchProduct(string searchType, string searchValue, int quarter, int year);

        Task<bool> CheckNewQuarterAsync();

        Task<byte[]> ExportDataToExcel(int quarter, int year);
    }
}
