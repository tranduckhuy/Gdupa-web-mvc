using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class SupplierController : Controller
	{
		private readonly ILogger<SupplierController> _logger;

		public SupplierController(ILogger<SupplierController> logger)
		{
			_logger = logger;
		}

		public IActionResult SupplierGrid()
		{
			return View();
		}

		public IActionResult SupplierList()
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
