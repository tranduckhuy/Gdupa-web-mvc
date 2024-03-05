using Warehouse.Domain.DTOs.ProductDTO;

namespace Warehouse.Domain.ViewModels
{
    public class ProductViewModel
    {
        public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public Pageable Pageable { get; set; } = null!;
    }
}
