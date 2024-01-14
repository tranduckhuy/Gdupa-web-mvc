using WarehouseWebMVC.Models.Domain;

namespace WarehouseWebMVC.Models.DTOs
{
    public class ProductDTO
	{
		public long ProductId { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public double Price { get; set; }
		public string Unit { get; set; } = string.Empty;
		public Category Category {  get; set; } = null!;
		public Brand Brand {  get; set; } = null!;
		public Supplier Supplier {  get; set; } = null!;
		public DateTime CreatedAt { get; set; }
		public DateTime ModifiedAt { get; set; }

		public ICollection<ProductImg> ProductImgs { get; set; } = new List<ProductImg>();
	}
}
