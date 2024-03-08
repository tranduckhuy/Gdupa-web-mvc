﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Warehouse.Domain.Entities;
using Warehouse.Infrastructure.Data;
using Warehouse.Service.Interfaces.Services;
using Warehouse.Shared;
using Warehouse.Shared.DTOs.ProductDTO;
using Warehouse.Shared.ViewModels;

namespace WarehouseWebMVC.Services;

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
                _dataContext.ProductImgs.Add(new ProductImg
                {
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

    public bool DiscontinuedProduct(long productId)
    {
        var product = _dataContext.Products.Find(productId);
        if (product == null)
        {
            return false;
        }
        product.IsDiscontinued = true;

        _dataContext.Entry(product).State = EntityState.Modified;
        _dataContext.SaveChanges();
        return true;
    }

    public bool ContinueProduct(long productId)
    {
        var product = _dataContext.Products.Find(productId);
        if (product == null)
        {
            return false;
        }
        product.IsDiscontinued = false;

        _dataContext.Entry(product).State = EntityState.Modified;
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
            .Include(p => p.ProductImgs)
            .FirstOrDefault(p => p.ProductId == productId);

        return product != null ? _mapper.Map<ProductDTO>(product) : null!;
    }

    public CRUProductVM GetInfoAddProduct()
    {
        var categories = _dataContext.Category.ToList();
        var brands = _dataContext.Brand.ToList();
        var units = new List<SelectListItem>();
        units.Add(new SelectListItem { Text = "Piece", Value = "Piece" });
        units.Add(new SelectListItem { Text = "Pair", Value = "Pair" });

        return new CRUProductVM
        {
            Categories = categories,
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
            .Include(p => p.ProductImgs);

        switch (searchType)
        {
            case "Category":
                searchProduct = searchProduct.Where(p => p.Category.Name.ToUpper().Contains(searchValue.ToUpper()) && p.IsDiscontinued == false);
                break;
            case "Brand":
                searchProduct = searchProduct.Where(p => p.Brand.Name.ToUpper().Contains(searchValue.ToUpper()) && p.IsDiscontinued == false);
                break;
            default:
                searchProduct = searchProduct.Where(p => p.Name.ToUpper().Contains(searchValue.ToUpper()) && p.IsDiscontinued == false);
                break;
        }

        if (searchProduct.Any())
        {
            var searchProductsDto = _mapper.Map<List<ProductDTO>>(searchProduct.ToList());
            var productViewModel = new ProductViewModel { Products = searchProductsDto };
            return productViewModel;
        }
        return null!;
    }

    public ProductViewModel SearchProductLock(string searchType, string searchValue)
    {
        IQueryable<Product> searchProduct = _dataContext.Products
            .Include(p => p.Category)
            .Include(p => p.Brand)
            .Include(p => p.ProductImgs);

        switch (searchType)
        {
            case "Category":
                searchProduct = searchProduct.Where(p => p.Category.Name.ToUpper().Contains(searchValue.ToUpper()) && p.IsDiscontinued == true);
                break;
            case "Brand":
                searchProduct = searchProduct.Where(p => p.Brand.Name.ToUpper().Contains(searchValue.ToUpper()) && p.IsDiscontinued == true);
                break;
            default:
                searchProduct = searchProduct.Where(p => p.Name.ToUpper().Contains(searchValue.ToUpper()) && p.IsDiscontinued == true);
                break;
        }

        if (searchProduct.Any())
        {
            var searchProductsDto = _mapper.Map<List<ProductDTO>>(searchProduct.ToList());
            var productViewModel = new ProductViewModel { Products = searchProductsDto };
            return productViewModel;
        }
        return null!;
    }

    public bool AddCategory(string categoryName)
    {
        try
        {
            if (string.IsNullOrEmpty(categoryName) || IsCategoryNameExist(categoryName.Trim()))
            {
                return false;
            }
            var category = new Category { Name = categoryName };
            _dataContext.Category.Add(category);
            _dataContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
    public bool AddBrand(string brandName)
    {
        try
        {
            if (string.IsNullOrEmpty(brandName) || IsBrandNameExist(brandName.Trim()))
            {
                return false;
            }
            var brand = new Brand { Name = brandName };
            _dataContext.Brand.Add(brand);
            _dataContext.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public int CountProductLock()
    {
        var lockedProducts = _dataContext.Products.Count(s => s.IsDiscontinued);
        return lockedProducts;
    }

    public int CountProductNotLock()
    {
        var unlockedProducts = _dataContext.Products.Count(s => !s.IsDiscontinued);
        return unlockedProducts;
    }

    private bool IsCategoryNameExist(string categoryName)
    {
        return _dataContext.Category.Any(c => c.Name.ToUpper() == categoryName.ToUpper());
    }

    private bool IsBrandNameExist(string brandName)
    {
        return _dataContext.Brand.Any(b => b.Name.ToUpper() == brandName.ToUpper());
    }

}
