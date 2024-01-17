using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services
{
    public interface IProductService
	{
		ProductViewModel GetAll(int page);
        CRUProductVM GetByIdForCRU(long productId);
        ProductDTO GetById(long productId);
        CRUProductVM Add(CRUProductVM addProductVM);
		bool Update(CRUProductVM addProductVM);
		bool Delete(long id);
		CRUProductVM GetInfoAddProduct();
    }
}
