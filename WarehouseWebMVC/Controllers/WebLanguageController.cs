using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class WebLanguageController : Controller
	{
		private readonly ILogger<WebLanguageController> _logger;

		public WebLanguageController(ILogger<WebLanguageController> logger)
		{
			_logger = logger;
		}

		public IActionResult WebLanguage()
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
