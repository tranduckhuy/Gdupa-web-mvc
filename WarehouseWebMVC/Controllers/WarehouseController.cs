using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Services;

namespace WarehouseWebMVC.Controllers
{
	public class WarehouseController : Controller
	{
		private readonly ILogger<WarehouseController> _logger;
        private readonly IWarehouseService _warehouseService;

        public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService warehouseService)
		{
			_logger = logger;
            _warehouseService = warehouseService;
        }

		[Filter]
		public IActionResult Warehouse(int page = 1)
		{
            if (HttpContext.Session.GetString("User") != null)
            {
                Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
                Response.Headers.Add("Pragma", "no-cache");
                Response.Headers.Add("Expires", "0");
				var warehouse = _warehouseService.GetAll(page);
                return View(warehouse);
            }
            return RedirectToAction("Login", "Authentication");
        }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
