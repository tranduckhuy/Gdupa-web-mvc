using Microsoft.AspNetCore.Mvc;
using Warehouse.Infrastructure;
using Warehouse.Infrastructure.Utils.Helper;
using Warehouse.Service.Interfaces.Services;
using Warehouse.Shared.DTOs;
using Warehouse.Shared.ViewModels;
using WarehouseWebMVC.AuthenticationFilter;

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
            SupplierViewModel supplierViewModel = _supplierService.GetLimit(page, false);
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
            SupplierViewModel supplierViewModel = _supplierService.GetLimit(page, false);
            ViewBag.Count = _supplierService.CountSupplierNotArchived();
            return View(supplierViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult SupplierArchive(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            SupplierViewModel supplierViewModel = _supplierService.GetLimit(page, true);
            ViewBag.Count = _supplierService.CountSupplierArchived();
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
        ModelState.Remove("Ward");
        ModelState.Remove("Apartment");
        if (ModelState.IsValid)
        {
            var currentSupplier = _supplierService.GetById(updateSupplierDTO.SupplierId);

            if (currentSupplier.Email != null && _supplierService.SupplierOwnsInformation(currentSupplier.Email, currentSupplier.SupplierId))
            {
                if (!_supplierService.UpdateSupplier(updateSupplierDTO))
                {
                    TempData["Message"] = AppConstant.MESSAGE_WRONG_INFO;
                    return RedirectToAction("SupplierInformation", new { supplierId = updateSupplierDTO.SupplierId });
                }
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return RedirectToAction("SupplierInformation", new { supplierId = updateSupplierDTO.SupplierId });
            }
            else
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("SupplierInformation", new { supplierId = updateSupplierDTO.SupplierId });
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("SupplierInformation", new { supplierId = updateSupplierDTO.SupplierId });
    }

    [HttpGet]
    public IActionResult DeactiveSupplier(long supplierId)
    {
        if (_supplierService.Deactive(supplierId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("SupplierList");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("SupplierList");
    }

    [HttpGet]
    public IActionResult ActiveSupplier(long supplierId)
    {
        if (_supplierService.Active(supplierId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("SupplierArchive");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("SupplierArchive");
    }

    [HttpPost]
    public IActionResult SearchSupplier(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchSuppliers = _supplierService.SearchSupplier(searchType, searchValue, false);
            if (searchSuppliers != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                ViewBag.SearchType = searchType;
                ViewBag.Count = _supplierService.CountSupplierNotArchived();
                return View("SupplierList", searchSuppliers);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        ViewBag.SearchType = searchType;
        ViewBag.Count = _supplierService.CountSupplierNotArchived();
        var page = 1;
        var allSupplier = _supplierService.GetLimit(page, false);
        return View("SupplierList", allSupplier);
    }

    [HttpPost]
    public IActionResult SearchSupplierArchive(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchSuppliers = _supplierService.SearchSupplier(searchType, searchValue, true);
            if (searchSuppliers != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                ViewBag.SearchType = searchType;
                ViewBag.Count = _supplierService.CountSupplierArchived();
                return View("SupplierArchive", searchSuppliers);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        ViewBag.SearchType = searchType;
        ViewBag.Count = _supplierService.CountSupplierArchived();
        var page = 1;
        var allSupplier = _supplierService.GetLimit(page, true);
        return View("SupplierArchive", allSupplier);
    }

}
