using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class CustomerController : Controller
	{
		private readonly ILogger<CustomerController> _logger;

		public CustomerController(ILogger<CustomerController> logger)
		{
			_logger = logger;
		}

		public IActionResult Customer()
		{
			return View();
		}

<<<<<<< Updated upstream:WarehouseWebMVC/Controllers/CustomerController.cs
		public IActionResult CustomerInformation()
=======
		public IActionResult UserInformation()
>>>>>>> Stashed changes:WarehouseWebMVC/Controllers/UserController.cs
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
