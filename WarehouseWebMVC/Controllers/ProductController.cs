using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers;

public class ProductController : Controller
{
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [Filter]
    [HttpGet]
    public IActionResult Product(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            ProductViewModel productViewModel = _productService.GetAll(page);
            return View(productViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpGet]
    public IActionResult TestList(int page = 1)
    {
        ProductViewModel productViewModel = _productService.GetAll(page);
        return View(productViewModel);
    }

    [Filter]
    [HttpGet]
    public IActionResult AddProduct()
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            var addProductVM = _productService.GetInfoAddProduct();
            ViewBag.Suppliers = new SelectList(addProductVM.Suppliers, "SupplierId", "Name");
            ViewBag.Categories = new SelectList(addProductVM.Categories, "CategoryId", "Name");
            ViewBag.Brands = new SelectList(addProductVM.Brands, "BrandId", "Name");
            ViewBag.Units = addProductVM.Units;
            return View();
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    [HttpGet]
    public IActionResult UpdateProduct(long productId)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            var updateProduct = _productService.GetByIdForCRU(productId);
            if (updateProduct.Product == null)
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("Product");
            }
            ViewBag.Suppliers = new SelectList(updateProduct.Suppliers, "SupplierId", "Name");
            ViewBag.Categories = new SelectList(updateProduct.Categories, "CategoryId", "Name");
            ViewBag.Brands = new SelectList(updateProduct.Brands, "BrandId", "Name");
            ViewBag.Units = updateProduct.Units;
            return View("UpdateProduct", updateProduct.Product);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    [HttpGet]
    public IActionResult ProductDetail(long productId)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            var product = _productService.GetById(productId);
            if (product == null)
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("Product");
            }
            return View(product);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult AddProduct([FromForm] AddProductDTO addProductDTO)
    {
        if (ModelState.IsValid)
        {
            if (_productService.Add(addProductDTO) != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return RedirectToAction("Product");
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        var addProductVM = _productService.GetInfoAddProduct();
        ViewBag.Suppliers = new SelectList(addProductVM.Suppliers, "SupplierId", "Name");
        ViewBag.Categories = new SelectList(addProductVM.Categories, "CategoryId", "Name");
        ViewBag.Brands = new SelectList(addProductVM.Brands, "BrandId", "Name");
        ViewBag.Units = addProductVM.Units;
        return View();
    }

    [HttpPost]
    public IActionResult UpdateProduct([FromForm] AddProductDTO updateProductDTO)
    {
        if (ModelState.IsValid)
        {
            if (_productService.Update(updateProductDTO))
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return RedirectToAction("Product");
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("UpdateProduct", "Product", new { productId = updateProductDTO.ProductId });
    }

    [HttpGet]
    public IActionResult DeleteProduct(long productId)
    {
        if (_productService.Delete(productId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Product");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Product");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]

    [HttpPost]
    public IActionResult SearchProduct(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchProducts = _productService.SearchProduct(searchType, searchValue);
            if (searchProducts != null)
            {
                return View("Product", searchProducts);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("Product");
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
