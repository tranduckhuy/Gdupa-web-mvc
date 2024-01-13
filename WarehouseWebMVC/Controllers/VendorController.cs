using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
    public class VendorController : Controller
    {
        private readonly ILogger<VendorController> _logger;

        public VendorController(ILogger<VendorController> logger)
        {
            _logger = logger;
        }

        public IActionResult VendorGrid()
        {
            return View();
        }

        public IActionResult VendorList()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
<<<<<<< Updated upstream
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
=======
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
>>>>>>> Stashed changes
}
