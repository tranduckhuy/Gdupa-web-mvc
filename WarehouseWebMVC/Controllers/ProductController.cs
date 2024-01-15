using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers
{
    //[Route("Product/[action]")]
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
        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpGet]
        public IActionResult UpdateProduct()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ProductDetail()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                if (_productService.Add(productDTO) != null)
                {
                    return RedirectToAction("Product");
                }
                else
                {
                    return View("AddProduct", new { Message = "Error", Data = productDTO });
                }

            }
            else
            {
                return View("AddProduct", new { Message = "Error" });
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct(ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                if (_productService.Update(productDTO))
                {
                    return RedirectToAction("Product");
                }
                else
                {
                    return View("UpdateProduct", new { Message = "Error", Data = productDTO });
                }

            }
            else
            {
                return View("UpdateProduct", new { Message = "Error" });
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(long productId)
        {
            if (ModelState.IsValid)
            {
                if (_productService.Delete(productId))
                {
                    return RedirectToAction("Product");
                }
                else
                {
                    // chưa xử lý
                    return RedirectToAction("Product");
                }
            }
            else
            {
                // chưa xử lý
                return RedirectToAction("Product");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Test
        [HttpGet]
        public IActionResult TestList(int page = 1)
        {
            ProductViewModel productViewModel = _productService.GetAll(page);
            return View(productViewModel);
        }

        //Test
        [HttpGet("{productId}")]
        public IActionResult TestDetail(long productId)
        {
            ProductDTO productDTO = _productService.GetById(productId);

            if (productDTO == null)
            {
                return RedirectToAction("TestList");
            }

            return View("TestDetail", productDTO);
        }
    }
}
