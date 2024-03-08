using Warehouse.Shared.DTOs;

namespace Warehouse.Shared.ViewModels
{
    public class SupplierViewModel
    {
        public ICollection<SupplierDTO> Suppliers { get; set; } = new List<SupplierDTO>();

        public Pageable Pageable { get; set; } = null!;
    }
}
