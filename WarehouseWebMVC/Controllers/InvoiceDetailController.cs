using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class InvoiceDetailController : Controller
	{
		private readonly ILogger<InvoiceDetailController> _logger;

		public InvoiceDetailController(ILogger<InvoiceDetailController> logger)
		{
			_logger = logger;
		}

		public IActionResult InvoiceDetail()
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
