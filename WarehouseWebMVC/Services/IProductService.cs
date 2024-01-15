using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
	public interface IProductService
	{
		ProductViewModel GetAll(int page);
		ProductDTO GetById(long productId);
		ProductDTO Add(ProductDTO productDTO);
		bool Update(ProductDTO productDTO);
		bool Delete(long id);
	}
}
