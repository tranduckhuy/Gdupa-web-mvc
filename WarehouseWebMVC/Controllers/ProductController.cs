using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.ProductDTO;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductService _productService;

        public ProductController(ILogger<ProductController> logger, IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        [HttpGet]
        public IActionResult Product(int page = 1)
        {
            ProductViewModel productViewModel = _productService.GetAll(page);
            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult TestList(int page = 1)
        {
            ProductViewModel productViewModel = _productService.GetAll(page);
            return View(productViewModel);
        }

        [HttpGet]
        public IActionResult AddProduct()
        {
            var addProductVM = _productService.GetInfoAddProduct();
            return View(addProductVM);
        }

        [HttpGet]
        public IActionResult UpdateProduct(long productId)
        {
            var updateProduct = _productService.GetById(productId);
            if (updateProduct.Product == null)
            {
                TempData["Message"] = "Product not found. Please select a valid product.";
                return RedirectToAction("Product");
            }
            return View("UpdateProduct", updateProduct);
        }

        [HttpGet]
        public IActionResult ProductDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct([FromForm] CRUProductVM addProductVM)
        {
            if (_productService.Add(addProductVM) != null)
            {
                TempData["Message"] = "Product added successfully";
                return RedirectToAction("Product");
            }
            TempData["Message"] = "Failed to add the product";
            return View("AddProduct", addProductVM);
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromForm] CRUProductVM addProductVM)
        {
            if (_productService.Add(addProductVM) != null)
            {
                TempData["Message"] = "Product updated successfully";
                return RedirectToAction("Product");
            }
            TempData["Message"] = "Failed to add the product";
            return View("UpdateProduct", addProductVM);
        }

        [HttpGet]
        public IActionResult DeleteProduct(long productId)
        {
            if (_productService.Delete(productId))
            {
                TempData["Message"] = "Product deleted successfully";
                return RedirectToAction("Product");
            }
            TempData["Message"] = "Failed to delete the product";
            return RedirectToAction("Product");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
