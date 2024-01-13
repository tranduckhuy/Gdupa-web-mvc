using WarehouseWebMVC.Models.DTOs;

namespace WarehouseWebMVC.Services
{
	public interface IProductService
	{
		List<ProductDTO> GetAll();
		ProductDTO GetById(long productId);
		ProductDTO Add(ProductDTO productDTO);
		bool Update(ProductDTO productDTO);
		bool Delete(long id);
	}
}
