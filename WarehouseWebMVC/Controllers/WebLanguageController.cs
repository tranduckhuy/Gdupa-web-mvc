using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.AuthenticationFilter;

namespace WarehouseWebMVC.Controllers;

public class WebLanguageController : Controller
{
	private readonly ILogger<WebLanguageController> _logger;

	public WebLanguageController(ILogger<WebLanguageController> logger)
	{
		_logger = logger;
	}

	[Filter]
	public IActionResult WebLanguage()
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
