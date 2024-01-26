﻿using Microsoft.AspNetCore.Mvc;
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
    public IActionResult WarehouseProduct(int page = 1, int quarter = 0, int year = 0)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
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
        if (ModelState.IsValid)
        {
            Console.WriteLine("Error");
        }
        if (_warehouseService.Add(importProductsDTO))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("WarehouseProduct", "Warehouse");
        }
        else
        {
            TempData["Message"] = AppConstant.MESSAGE_FAILED;
            return RedirectToAction("WarehouseImport", "Warehouse");
        }
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
