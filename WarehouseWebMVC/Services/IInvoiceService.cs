using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IInvoiceService
    {
        SupplierViewModel GetAll(int page);
        bool Add(ImportProductsDTO importProducts);
    }
}
