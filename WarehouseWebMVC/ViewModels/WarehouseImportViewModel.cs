using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.ProductDTO;

namespace WarehouseWebMVC.ViewModels
{
	public class WarehouseImportViewModel
	{
		public ICollection<ProductDTO> Products { get; set; } = new List<ProductDTO>();
		public ICollection<Supplier> Suppliers { get; set; } = new List<Supplier>();

	}
}
