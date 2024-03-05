using Warehouse.Domain.DTOs;
using Warehouse.Domain.ViewModels;

namespace Warehouse.Domain.Interfaces
{
    public interface IWarehouseService
    {
        Task<WarehouseViewModel> GetLimitAsync(int page, int quarter, int year);

        Task<WarehouseViewModel> GetByStatusAsync(string status);

        bool Add(ImportProductsDTO importProducts);

        Task<WarehouseImportViewModel> GetDataViewImportAsync();

        WarehouseViewModel SearchProduct(string searchType, string searchValue);

        Task<bool> CheckNewQuarterAsync();

        Task<byte[]> ExportDataToExcel(int quarter, int year);
    }
}
