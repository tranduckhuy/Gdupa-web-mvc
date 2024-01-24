using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IWarehouseService
    {
        WarehouseViewModel GetAll(int page);
        bool Add(ImportProductsDTO importProducts);
        WarehouseImportViewModel GetDataViewImport();
    }
}
