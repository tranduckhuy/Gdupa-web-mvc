using Warehouse.Domain.Entities;

namespace Warehouse.Shared.DTOs.ProductDTO
{
    public class AddProductDTO
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public string ImageURL1 { get; set; } = string.Empty;
        public string ImageURL2 { get; set; } = string.Empty;
        public ICollection<ProductImg> ProductImgs { get; set; } = new List<ProductImg>();
    }
}
