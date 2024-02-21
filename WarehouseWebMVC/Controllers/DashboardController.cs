using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.Services.Impl;

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
    public IActionResult Dashboard(int year = 0)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            ViewBag.User = HttpContext.Session.GetString("User");
            ViewBag.Name = HttpContext.Session.GetString("Name");
            ViewBag.Address = HttpContext.Session.GetString("Address");
            DashboardDTO dashboardDTO = _dashboardService.GetDashboardInfo(year);
            return View(dashboardDTO);
        }
        return RedirectToAction("Login", "Authentication");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
