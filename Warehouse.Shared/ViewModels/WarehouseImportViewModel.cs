using Warehouse.Domain.Entities;
using Warehouse.Shared.DTOs.ProductDTO;

namespace Warehouse.Shared.ViewModels
{
    public class WarehouseImportViewModel
    {
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    }
}
