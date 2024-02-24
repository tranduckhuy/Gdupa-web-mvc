using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IProductService
	{
		ProductViewModel GetAll(int page);
        CRUProductVM GetByIdForCRU(long productId);
        ProductDTO GetById(long productId);
        ProductViewModel SearchProduct(string searchType, string searchValue);
        AddProductDTO Add(AddProductDTO addProductDTO);
        bool AddCategory(string categoryName);
        bool AddBrand(string brandName);
        bool Update(AddProductDTO addProductDTO);
		bool Delete(long id);
		CRUProductVM GetInfoAddProduct();
    }
}
