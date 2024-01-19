using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IProductService
	{
		ProductViewModel GetAll(int page);
        CRUProductVM GetByIdForCRU(long productId);
        ProductDTO GetById(long productId);
        AddProductDTO Add(AddProductDTO addProductDTO);
		bool Update(AddProductDTO addProductDTO);
		bool Delete(long id);
		CRUProductVM GetInfoAddProduct();
    }
}
