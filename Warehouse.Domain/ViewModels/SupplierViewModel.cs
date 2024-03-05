using Warehouse.Domain.DTOs;

namespace Warehouse.Domain.ViewModels
{
    public class SupplierViewModel
    {
        public ICollection<SupplierDTO> Suppliers { get; set; } = new List<SupplierDTO>();

        public Pageable Pageable { get; set; } = null!;
    }
}
