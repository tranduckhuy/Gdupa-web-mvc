﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Services;
using WarehouseWebMVC.Services.Impl;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers;

public class ImportNoteController : Controller
{
    private readonly ILogger<ImportNoteController> _logger;
    private readonly IImportNoteService _importNoteService;

    public ImportNoteController(ILogger<ImportNoteController> logger, IImportNoteService importNoteService)
    {
        _logger = logger;
        _importNoteService = importNoteService;
    }

    [Filter]
    public IActionResult ImportNoteList(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            ImportNoteViewModel importNoteViewModel = _importNoteService.GetAll(page);
            return View(importNoteViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult ImportNoteDetail(long importNoteId)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");

            var importNoteDetailVM = _importNoteService.GetDetailById(importNoteId);

            if (importNoteDetailVM == null)
            {
                TempData["Message"] = AppConstant.MESSAGE_FAILED;
                return RedirectToAction("ImportNoteList", "ImportNote");
            }
            return View(importNoteDetailVM);
        }

        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [HttpPost]
    public IActionResult SearchImportNote(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchImportNotes = _importNoteService.SearchImportNote(searchType, searchValue);
            if (searchImportNotes != null)
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                ViewBag.SearchType = searchType;
                return View("ImportNoteList", searchImportNotes);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("ImportNoteList");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
