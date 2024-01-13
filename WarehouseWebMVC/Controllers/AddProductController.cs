using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class AddProductController : Controller
	{
		private readonly ILogger<AddProductController> _logger;

		public AddProductController(ILogger<AddProductController> logger)
		{
			_logger = logger;
		}

		public IActionResult AddProduct()
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
