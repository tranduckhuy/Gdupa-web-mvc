using Warehouse.Shared.DTOs.ProductDTO;
using Warehouse.Shared.ViewModels;

namespace Warehouse.Service.Interfaces.Services
{
    public interface IProductService
    {
        ProductViewModel GetLimit(int page, bool isDiscontinued);
        CRUProductVM GetByIdForCRU(long productId);
        ProductDTO GetById(long productId);
        ProductViewModel SearchProduct(string searchType, string searchValue, bool isContinued);
        AddProductDTO Add(AddProductDTO addProductDTO);
        bool AddCategory(string categoryName);
        bool AddBrand(string brandName);
        bool Update(AddProductDTO addProductDTO);
        bool DiscontinuedProduct(long id);
        bool ContinueProduct(long id);
        CRUProductVM GetInfoAddProduct();
        int CountProductLock();
        int CountProductNotLock();
    }
}
