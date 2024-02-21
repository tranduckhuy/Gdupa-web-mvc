using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers;

public class WarehouseController : Controller
{
    private readonly ILogger<WarehouseController> _logger;
    private readonly IWarehouseService _warehouseService;

    public WarehouseController(ILogger<WarehouseController> logger, IWarehouseService warehouseService)
    {
        _logger = logger;
        _warehouseService = warehouseService;
    }

    [Filter]
    [HttpGet]
    public IActionResult WarehouseProduct(int page = 1, int quarter = 0, int year = 0)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            if (_warehouseService.CheckNewQuarter())
            {
                TempData["Message"] = AppConstant.NEW_QUARTER;
            }
            var warehouse = _warehouseService.GetLimit(page, quarter, year);
            if (warehouse == null)
            {
                TempData["Message"] = AppConstant.BAD_REQUEST;
                return RedirectToAction("WarehouseProduct", "Warehouse");
            }
            return View(warehouse);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    [HttpGet]
    [Route("Warehouse/WarehouseProduct/WarehouseProductStatus")]
    public IActionResult WarehouseProductStatus(string status)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("WarehouseProduct", "Warehouse");
        }

        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            var warehouse = _warehouseService.GetByStatus(status);
            if (warehouse == null)
            {
                TempData["Message"] = AppConstant.BAD_REQUEST;
                return RedirectToAction("WarehouseProduct", "Warehouse");
            }
            return View(warehouse);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    [HttpGet]
    public IActionResult WarehouseImport()
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            var warehouseImportVM = _warehouseService.GetDataViewImport();

            return View(warehouseImportVM);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult WarehouseImport([FromBody] ImportProductsDTO importProductsDTO)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            if (_warehouseService.Add(importProductsDTO))
            {
                return Json(new { success = true, loggedIn = true });
            }
            else
            {
                return Json(new { success = false, loggedIn = true });
            }
        }

        return Json(new { success = false, loggedIn = false });
    }

    [HttpPost]
    public IActionResult SearchProduct(string searchType, string searchValue)
    {
        if (!ModelState.IsValid)
        {
            TempData["Message"] = AppConstant.BAD_REQUEST;
            return RedirectToAction("WarehouseProduct", "Warehouse");
        }
        var warehouseProduct = _warehouseService.SearchProduct(searchType, searchValue);
        if (warehouseProduct != null)
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            ViewBag.SearchType = searchType;
            return View("WarehouseProduct", warehouseProduct);
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("WarehouseProduct", "Warehouse");
    }
    [HttpPost]
    public async Task<IActionResult> ExportFile(int quarter, int year)
    {
        var fileBytes = await _warehouseService.ExportDataToExcel(quarter, year);

        return File(fileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "WarehouseData.xlsx");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
