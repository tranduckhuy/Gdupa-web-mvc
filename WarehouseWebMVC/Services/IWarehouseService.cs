using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IWarehouseService
    {
        WarehouseViewModel GetLimit(int page, int quarter, int year);
        WarehouseViewModel GetByStatus(string status);
        bool Add(ImportProductsDTO importProducts);
        WarehouseImportViewModel GetDataViewImport();
        WarehouseViewModel SearchProduct(string searchType, string searchValue);
    }
}
