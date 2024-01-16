using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs.ProductDTO

{
    public class CURProductDTO
    {
        public long ProductId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public string Unit { get; set; } = string.Empty;
        public long CategoryId { get; set; }
        public long BrandId { get; set; }
        public long SupplierId { get; set; }
        public ICollection<ProductImg> ProductImgs { get; set; } = new List<ProductImg>();
    }
}
