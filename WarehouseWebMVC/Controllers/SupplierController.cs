using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.Services.Helper;
using WarehouseWebMVC.Services.Impl;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers;

public class SupplierController : Controller
{
    private readonly ILogger<SupplierController> _logger;
    private readonly ISupplierService _supplierService;

    public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService)
    {
        _logger = logger;
        _supplierService = supplierService;
    }

    [Filter]
    public IActionResult SupplierGrid(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            SupplierViewModel supplierViewModel = _supplierService.GetAll(page);
            return View(supplierViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult SupplierList(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            SupplierViewModel supplierViewModel = _supplierService.GetAll(page);
            return View(supplierViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult SupplierInformation()
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

    [HttpPost]
    public IActionResult SearchSupplier(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchSuppliers = _supplierService.SearchSupplier(searchType, searchValue);
            if (searchSuppliers != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                ViewBag.SearchType = searchType;
                return View("SupplierList", searchSuppliers);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("SupplierList");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
