using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl
{
    public class ProductService : IProductService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ProductService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public CRUProductVM Add(CRUProductVM AddProductVM)
        {
            try
            {
                var product = _mapper.Map<Product>(AddProductVM.Product);
                if (product != null)
                {
                    _dataContext.Products.Add(product);
                    _dataContext.SaveChanges();
                    return AddProductVM;
                }
                return null!;
            }
            catch (Exception)
            {
                return null!;
            }
        }

        public bool Delete(long productId)
        {
            var product = _dataContext.Products.Find(productId);
            if (product == null)
            {
                return false;
            }
            _dataContext.Products.Remove(product);
            _dataContext.SaveChanges();
            return true;
        }

        public ProductViewModel GetAll(int page)
        {

            var totalProducts = _dataContext.Products.Count();
            const int pageSize = 2;
            if (page < 1)
            {
                page = 1;
            }
            var pageable = new Pageable(totalProducts, page, pageSize);

            int skipAmount = (pageable.CurrentPage - 1) * pageSize;

            var products = _dataContext.Products
                .Skip(skipAmount)
                .Take(pageSize)
                .Include(p => p.Supplier)
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.ProductImgs)
                .OrderBy(p => p.ProductId)
                .ToList();

            var productsDto = _mapper.Map<List<ProductDTO>>(products);

            var productViewModel = new ProductViewModel { Products = productsDto, Pageable = pageable };

            return productViewModel;
        }

        public CRUProductVM GetById(long id)
        {
            var product = _dataContext.Products
                .Include(p => p.Category)
                .Include(p => p.Brand)
                .Include(p => p.Supplier)
                .Include(p => p.ProductImgs)
                .FirstOrDefault(p => p.ProductId == id);

            var productDTO = product != null ? _mapper.Map<CURProductDTO>(product) : null!;
            if (productDTO != null)
            {
                var productVM = GetInfoAddProduct();
                productVM.Product = productDTO;
                return productVM;
            }
            return null!;
        }

        public CRUProductVM GetInfoAddProduct()
        {
            var categories = _dataContext.Category.ToList();
            var suplliers = _dataContext.Suppliers.ToList();
            var brands = _dataContext.Brand.ToList();

            return new CRUProductVM
            {
                Categorys = categories,
                Suppliers = suplliers,
                Brands = brands
            };
        }

        public bool Update(CRUProductVM addProductVM)
        {
            try
            {
                var existingProduct = _dataContext.Products.FirstOrDefault(p => p.ProductId == addProductVM.Product.ProductId);

                if (existingProduct == null)
                {
                    return false;
                }
                var product = _mapper.Map<Product>(addProductVM.Product);
                _dataContext.Entry(product).State = EntityState.Modified;
                _dataContext.SaveChanges();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
