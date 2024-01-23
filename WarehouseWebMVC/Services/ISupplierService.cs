using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface ISupplierService
    {
        SupplierViewModel GetAll(int page);
        Supplier Add(Supplier supplier);
    }
}
