using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
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

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
