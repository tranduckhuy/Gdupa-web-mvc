using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class VendorGridController : Controller
	{
		private readonly ILogger<VendorGridController> _logger;

		public VendorGridController(ILogger<VendorGridController> logger)
		{
			_logger = logger;
		}

		public IActionResult VendorGrid()
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
