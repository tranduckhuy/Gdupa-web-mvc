using Warehouse.Shared.DTOs.ProductDTO;

namespace Warehouse.Shared.ViewModels
{
    public class ProductViewModel
    {
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public Pageable Pageable { get; set; } = null!;
    }
}
