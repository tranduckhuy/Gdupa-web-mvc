using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.ViewModels
{
    public class ProductViewModel
    {
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public Pageable Pageable { get; set; } = null!;
    }
}
