using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;
using WarehouseWebMVC.Data;
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
                return View(addProductVM);
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
                    TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                    return RedirectToAction("Product");
                }
                return View("UpdateProduct", updateProduct);
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
        public IActionResult AddProduct([FromForm] CRUProductVM addProductVM)
        {
            if (_productService.Add(addProductVM) != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return RedirectToAction("Product");
            }
            TempData["Message"] = AppConstant.MESSAGE_FAILED;
            return View("AddProduct", addProductVM);
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromForm] CRUProductVM addProductVM)
        {
            if (_productService.Add(addProductVM) != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return RedirectToAction("Product");
            }
            TempData["Message"] = AppConstant.MESSAGE_FAILED;
            return View("UpdateProduct", addProductVM);
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
