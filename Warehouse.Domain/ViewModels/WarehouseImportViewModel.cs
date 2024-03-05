using Warehouse.Domain.DTOs.ProductDTO;
using Warehouse.Domain.Entities;

namespace Warehouse.Domain.ViewModels
{
    public class WarehouseImportViewModel
    {
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

    }
}
