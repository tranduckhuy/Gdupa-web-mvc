using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IWarehouseService
    {
        WarehouseViewModel GetLimit(int page, int quarter, int year);
        bool Add(ImportProductsDTO importProducts);
        WarehouseImportViewModel GetDataViewImport();
    }
}
