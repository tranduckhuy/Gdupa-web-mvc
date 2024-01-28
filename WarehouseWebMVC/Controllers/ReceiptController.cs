using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.Services.Impl;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers;

public class ReceiptController : Controller
{
    private readonly ILogger<ReceiptController> _logger;
    private readonly IReceiptService _receiptService;

    public ReceiptController(ILogger<ReceiptController> logger, IReceiptService receiptService)
    {
        _logger = logger;
        _receiptService = receiptService;
    }

    [Filter]
    public IActionResult ReceiptList(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            ReceiptViewModel receiptViewModel = _receiptService.GetAll(page);
            return View(receiptViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult ReceiptDetail(long receiptId)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            var receiptDetailVM = _receiptService.GetDetailById(receiptId);

            if (receiptDetailVM == null)
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("ReceiptList", "Receipt");
            }
            return View(receiptDetailVM);
        }

        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult SearchReceipt(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchReceipts = _receiptService.SearchReceipt(searchType, searchValue);
            if (searchReceipts != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return View("ReceiptList", searchReceipts);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("ReceiptList");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
