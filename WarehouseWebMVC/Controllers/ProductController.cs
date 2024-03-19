using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using Warehouse.Infrastructure;
using Warehouse.Service.Interfaces.Services;
using Warehouse.Shared.DTOs.ProductDTO;
using Warehouse.Shared.ViewModels;
using WarehouseWebMVC.AuthenticationFilter;

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
            ProductViewModel productViewModel = _productService.GetLimit(page, false);
            ViewBag.Count = _productService.CountProductNotLock();
            return View(productViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    [HttpGet]
    public IActionResult StopSellingProducts(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            ProductViewModel productViewModel = _productService.GetLimit(page, true);
            ViewBag.Count = _productService.CountProductLock();
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
        return View(addProductDTO);
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
    public IActionResult DiscontinuedProduct(long productId)
    {
        if (_productService.DiscontinuedProduct(productId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("ProductList");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("ProductList");
    }

    public IActionResult ContinueProduct(long productId)
    {
        if (_productService.ContinueProduct(productId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("StopSellingProducts");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("StopSellingProducts");
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
                ViewBag.Count = _productService.CountProductNotLock();
                return View("ProductList", searchProducts);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        ViewBag.SearchType = searchType;
        ViewBag.Count = _productService.CountProductNotLock();
        var page = 1;
        var isContinue = true;
        var allProduct = _productService.GetLimit(page, isContinue);
        return View("ProductList", allProduct);
    }

    [HttpPost]
    public IActionResult SearchProductLock(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchProducts = _productService.SearchProductLock(searchType, searchValue);
            if (searchProducts != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                ViewBag.SearchType = searchType;
                ViewBag.Count = _productService.CountProductLock();
                return View("StopSellingProducts", searchProducts);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        ViewBag.SearchType = searchType;
        ViewBag.Count = _productService.CountProductLock();
        var page = 1;
        var isContinue = false;
        var allProduct = _productService.GetLimit(page, isContinue);
        return View("StopSellingProducts", allProduct);
    }

}
