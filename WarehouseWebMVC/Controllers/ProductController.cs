using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
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
    public IActionResult ProductList(int page = 1)
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
                return RedirectToAction("ProductList");
            }
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
                return RedirectToAction("ProductList");
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
                return RedirectToAction("ProductList");
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        var addProductVM = _productService.GetInfoAddProduct();
        ViewBag.Categories = new SelectList(addProductVM.Categories, "CategoryId", "Name");
        ViewBag.Brands = new SelectList(addProductVM.Brands, "BrandId", "Name");
        ViewBag.Units = addProductVM.Units;
        return View();
    }
    [HttpPost]
    public IActionResult AddCategory([FromBody] JObject data)
    {
        string categoryName = data["categoryName"]!.ToString();
        if (!string.IsNullOrEmpty(categoryName))
        {
            if (_productService.AddCategory(categoryName))
            {
                var addProductVM = _productService.GetInfoAddProduct(); 
                return Json(new { success = true, category = new { addProductVM.Categories.Last().CategoryId, addProductVM.Categories.Last().Name } });
            }
        }
        return Json(new { success = false });
    }
    [HttpPost]
    public IActionResult AddBrand([FromBody] JObject data)
    {
        string brandName = data["brandName"]!.ToString();
        if (!string.IsNullOrEmpty(brandName))
        {
            if (_productService.AddBrand(brandName))
            {
                var addProductVM = _productService.GetInfoAddProduct();
                return Json(new { success = true, brand = new { addProductVM.Brands.Last().BrandId, addProductVM.Brands.Last().Name } });
            }
        }
        return Json(new { success = false });
    }

    [HttpPost]
    public IActionResult AddUnit([FromBody] JObject data)
    {
        string unitName = data["unitName"]!.ToString();
        if (!string.IsNullOrEmpty(unitName))
        {
            if (_productService.AddUnit(unitName))
            {
                var productVM = _productService.GetInfoAddProduct();
                return Json(new { success = true, unit = new { productVM.Units.Last().Value, productVM.Units.Last().Text } });
            }
        }
        return Json(new { success = false });
    }

    [HttpPost]
    public IActionResult UpdateProduct([FromForm] AddProductDTO updateProductDTO)
    {
        if (_productService.Update(updateProductDTO))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("ProductList");
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
            return RedirectToAction("ProductList");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("ProductList");
    }

    [HttpPost]
    public IActionResult SearchProduct(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchProducts = _productService.SearchProduct(searchType, searchValue);
            if (searchProducts != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                ViewBag.SearchType = searchType;
                return View("ProductList", searchProducts);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("ProductList");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
