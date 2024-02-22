using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs.SupplierDTO;
using WarehouseWebMVC.Models.DTOs.UserDTO;
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
    private readonly IAddressHelper _addressHelper;

    public SupplierController(ILogger<SupplierController> logger, ISupplierService supplierService, IAddressHelper addressHelper)
    {
        _logger = logger;
        _supplierService = supplierService;
        _addressHelper = addressHelper;
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
    public IActionResult SupplierInformation(long supplierId)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            var supplier = _supplierService.GetById(supplierId);
            if (supplier == null)
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("SupplierList");
            }
            return View(supplier);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    [HttpGet]
    public IActionResult AddSupplier()
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            return View();
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult AddSupplier(SupplierDTO addSupplierDTO)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            ModelState.Remove("Ward");
            ModelState.Remove("Apartment");
            if (_supplierService.IsEmailAlreadyExists(addSupplierDTO.Email))
            {
                TempData["Message"] = AppConstant.MESSAGE_WRONG_INFO;
                return View(addSupplierDTO);
            }
            if (ModelState.IsValid)
            {
                addSupplierDTO.Avatar ??= "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";
                addSupplierDTO.Ward ??= "";
                addSupplierDTO.Apartment ??= "";

                if (_supplierService.AddSupplier(addSupplierDTO))
                {
                    TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                    return RedirectToAction("SupplierList");
                }
            }

            TempData["Message"] = AppConstant.MESSAGE_FAILED;
            return View(addSupplierDTO);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }
    [HttpPost]
    public IActionResult UpdateSupplier(SupplierDTO updateSupplierDTO)
    {
        if (ModelState.IsValid)
        {
            var currentSupplier = _supplierService.GetById(updateSupplierDTO.SupplierId);

            if (currentSupplier.Email != null && _supplierService.SupplierOwnsInformation(currentSupplier.Email, currentSupplier.SupplierId))
            {
                if (!_supplierService.UpdateSupplier(updateSupplierDTO))
                {
                    TempData["Message"] = AppConstant.MESSAGE_WRONG_INFO;
                    return RedirectToAction("SupplierInformation", new {supplierId = updateSupplierDTO.SupplierId});
                }
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return RedirectToAction("SupplierInformation", new {supplierId = updateSupplierDTO.SupplierId});
            }
            else
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("SupplierInformation", new {supplierId = updateSupplierDTO.SupplierId});
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("SupplierInformation", new {supplierId = updateSupplierDTO.SupplierId});
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
