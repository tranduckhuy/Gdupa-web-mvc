using Microsoft.AspNetCore.Mvc;
using WarehouseWebMVC.AuthenticationFilter;

namespace WarehouseWebMVC.Controllers;

public class FAQController : Controller
{
	private readonly ILogger<FAQController> _logger;

	public FAQController(ILogger<FAQController> logger)
	{
		_logger = logger;
	}

	[Filter]
	public IActionResult FAQ()
	{
            if (HttpContext.Session.GetString("User") != null)
            {
                Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
                Response.Headers.Add("Pragma", "no-cache");
                Response.Headers.Add("Expires", "0");
                return View();
            }
            return RedirectToAction("Login", "Authentication");
        }

}
