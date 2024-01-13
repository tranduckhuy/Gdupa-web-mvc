using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class UserInformationController : Controller
	{
		private readonly ILogger<UserInformationController> _logger;

		public UserInformationController(ILogger<UserInformationController> logger)
		{
			_logger = logger;
		}

		public IActionResult UserInformation()
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
