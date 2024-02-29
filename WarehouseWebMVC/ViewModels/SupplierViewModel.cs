using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs.SupplierDTO;

namespace WarehouseWebMVC.ViewModels
{
    public class SupplierViewModel
    {
        public ICollection<SupplierDTO> Suppliers { get; set; } = new List<SupplierDTO>();

        public Pageable Pageable { get; set; } = null!;
    }
}
