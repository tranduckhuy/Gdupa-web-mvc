using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.SupplierDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface ISupplierService
    {
        SupplierDTO GetById(long supplierId);
        SupplierViewModel GetAll(int page);
        SupplierViewModel SearchSupplier(string searchType, string searchValue);
        SupplierViewModel SearchSupplierArchive(string searchType, string searchValue);
        bool AddSupplier(SupplierDTO addSupplierDTO);
        bool UpdateSupplier(SupplierDTO updateSupplierDTO);
        bool IsEmailAlreadyExists(string email);
        bool SupplierOwnsInformation(string supplierEmail, long supplierId);
        bool Deactive(long supplierId);
        bool Active(long supplierId);
    }
}
