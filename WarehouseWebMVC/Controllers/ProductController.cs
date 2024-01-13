using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Services;

namespace WarehouseWebMVC.Controllers
{
<<<<<<< Updated upstream
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
		public IActionResult Product()
		{
			return View();
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
					return View("Product");
				} else
				{
					return View("AddProduct", new { Message = "Error", Data = productDTO });
				}
				
			} else
			{
				return View("AddProduct", new { Message = "Error" } );
			}
		}

		[HttpPut]
		public IActionResult UpdateProduct(ProductDTO productDTO)
		{
			if (ModelState.IsValid)
			{
				if (_productService.Update(productDTO))
				{
					return View("Product");
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
					return View("Product");
				}
				else
				{
					return View("Product", new { Message = "Error" });
				}
			}
			else
			{
				return View("Product", new { Message = "Error" });
			}
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
=======
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        public IActionResult Product()
        {
            return View();
        }

        public IActionResult ProductDetail()
        {
            return View();
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        public IActionResult UpdateProduct()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
>>>>>>> Stashed changes
}
