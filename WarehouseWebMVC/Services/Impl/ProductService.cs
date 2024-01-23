using AutoMapper;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Services.Impl;

public class ProductService : IProductService
{
    private readonly DataContext _dataContext;
    private readonly IMapper _mapper;

    public ProductService(DataContext dataContext, IMapper mapper)
    {
        _dataContext = dataContext;
        _mapper = mapper;
    }

    public AddProductDTO Add(AddProductDTO addProductDTO)
    {
        try
        {
            var product = _mapper.Map<Product>(addProductDTO);
            if (product != null)
            {
                // Add product base information
                _dataContext.Products.Add(product);
                _dataContext.SaveChanges();
                
                // Add product images
                var productId = product.ProductId;
                _dataContext.ProductImgs.Add(new ProductImg { 
                    ProductId = productId, 
                    ImageURL = addProductDTO.ImageURL1 
                });
                _dataContext.ProductImgs.Add(new ProductImg
                {
                    ProductId = productId,
                    ImageURL = addProductDTO.ImageURL2
                });
                _dataContext.SaveChanges();
                return addProductDTO;
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
        const int pageSize = 5;
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

    public CRUProductVM GetByIdForCRU(long productId)
    {
        var product = _dataContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Supplier)
            .Include(p => p.ProductImgs)
            .FirstOrDefault(p => p.ProductId == productId);

        var productDTO = product != null ? _mapper.Map<AddProductDTO>(product) : null!;
        if (productDTO != null)
        {
            var productVM = GetInfoAddProduct();
            productVM.Product = productDTO;
            return productVM;
        }
        return null!;
    }

    public ProductDTO GetById(long productId)
    {
        var product = _dataContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Supplier)
            .Include(p => p.ProductImgs)
            .FirstOrDefault(p => p.ProductId == productId);

        return product != null ? _mapper.Map<ProductDTO>(product) : null!;
    }

    public CRUProductVM GetInfoAddProduct()
    {
        var categories = _dataContext.Category.ToList();
        var suplliers = _dataContext.Suppliers.ToList();
        var brands = _dataContext.Brand.ToList();
        var units = new List<SelectListItem>();
        units.Add(new SelectListItem { Text = "Piece", Value = "Piece"});
        units.Add(new SelectListItem { Text = "Pair", Value = "Pair"});

        return new CRUProductVM
        {
            Categories = categories,
            Suppliers = suplliers,
            Brands = brands,
            Units = units
        };
    }

	public bool Update(AddProductDTO updateProductDTO)
	{
		try
		{
			var existingProduct = _dataContext.Products
				.Include(p => p.ProductImgs)
				.FirstOrDefault(p => p.ProductId == updateProductDTO.ProductId);

			if (existingProduct == null)
			{
				return false;
			}

			if (updateProductDTO.ImageURL1 != null)
			{
				existingProduct.ProductImgs.ElementAt(0).ImageURL = updateProductDTO.ImageURL1;
			}
			if (updateProductDTO.ImageURL2 != null)
			{
				existingProduct.ProductImgs.ElementAt(1).ImageURL = updateProductDTO.ImageURL2;
			}

			updateProductDTO.ProductImgs.Add(existingProduct.ProductImgs.ElementAt(0));
            updateProductDTO.ProductImgs.Add(existingProduct.ProductImgs.ElementAt(1));
			
			_mapper.Map(updateProductDTO, existingProduct);
            var product = existingProduct;
			_dataContext.SaveChanges();
			return true;
		}
		catch (Exception)
		{
			return false;
		}
	}

    public ProductViewModel SearchProduct(string searchType, string searchValue)
    {
        IQueryable<Product> searchProduct = _dataContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.Supplier)
            .Include(p => p.ProductImgs);

        if ("Name".Equals(searchType))
        {
            searchProduct = searchProduct.Where(p => p.Name.ToUpper().Contains(searchValue.ToUpper()));
        }
        else if ("Supplier".Equals(searchType))
        {
            searchProduct = searchProduct.Where(p => p.Supplier.Name.ToUpper().Contains(searchValue.ToUpper()));
        }

        if (searchProduct.Any())
        {
            var searchProductsDto = _mapper.Map<List<ProductDTO>>(searchProduct.ToList());
            var productViewModel = new ProductViewModel { Products = searchProductsDto };
            return productViewModel;
        }
        return null!;
    }
}
