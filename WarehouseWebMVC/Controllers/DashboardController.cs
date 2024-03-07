using Microsoft.AspNetCore.Mvc;
using Warehouse.Service.Interfaces.Services;
using Warehouse.Shared.DTOs;
using WarehouseWebMVC.AuthenticationFilter;

namespace WarehouseWebMVC.Controllers;

public class DashboardController : Controller
{
    private readonly ILogger<DashboardController> _logger;
    private readonly IDashboardService _dashboardService;

    public DashboardController(ILogger<DashboardController> logger, IDashboardService dashboardService)
    {
        _logger = logger;
        _dashboardService = dashboardService;
    }

    [Filter]
    public async Task<IActionResult> Dashboard(int year = 0)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
			Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            ViewBag.User = HttpContext.Session.GetString("User");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Address = HttpContext.Session.GetString("Address");
            DashboardDTO dashboardDTO = await _dashboardService.GetDashboardInfoAsync(year);

			return View(dashboardDTO);
        }
        return RedirectToAction("Login", "Authentication");
    }

}
