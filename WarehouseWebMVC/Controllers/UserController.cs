﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.ViewModels;

namespace WarehouseWebMVC.Controllers;

public class UserController : Controller
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [Filter]
    [HttpGet]
    public IActionResult Users(int page = 1)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            UserViewModel userViewModel = _userService.GetAll(page);
            return View(userViewModel);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult UserInformation(long userId)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            var userInformationVM = _userService.GetUserById(userId);
            return View(userInformationVM);
        }
        TempData["Message"] = AppConstant.MESSAGE_NOT_LOGIN;
        return RedirectToAction("Login", "Authentication");
    }


    [Filter]
    public IActionResult AddUser()
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
    public IActionResult AddUser(AddUserDTO addUserDTO)
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            if (ModelState.IsValid)
            {
                addUserDTO.Avatar ??= "https://firebasestorage.googleapis.com/v0/b/gdupa-2fa82.appspot.com/o/avatar%2Fdefault_avatar.png?alt=media&token=560b08e7-3ab2-453e-aea5-def178730766";
                addUserDTO.Role = "FE";
                addUserDTO.IsLocked = false;
                if (_userService.AddUser(addUserDTO))
                {
                    TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                    return RedirectToAction("Users");
                }
            }
            TempData["Message"] = AppConstant.MESSAGE_FAILED;
            return View(addUserDTO);
        }
        return RedirectToAction("Login", "Authentication");
    }


    [HttpPost]
    public IActionResult UserInformation(UserInformationDTO userInformationDTO)
    {
        if (ModelState.IsValid)
        {
            if (_userService.UpdateUser(userInformationDTO))
            {
                var user = _userService.GetUserById(userInformationDTO.UserId);
                if (user != null)
                {
                    byte[] userIdBytes = BitConverter.GetBytes(user.UserId);
                    HttpContext.Session.Set("Id", userIdBytes);
                    HttpContext.Session.SetString("Name", user.Name);
                    HttpContext.Session.SetString("User", user.Email);
                    HttpContext.Session.SetString("Address", user.Address);
                    HttpContext.Session.SetString("Avatar", user.Avatar);
                }
                else
                {
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    return View(_userService.GetUserById(userInformationDTO.UserId));
                }
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return View(_userService.GetUserById(userInformationDTO.UserId));
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return View(_userService.GetUserById(userInformationDTO.UserId));
    }

    [HttpPost]
    public IActionResult ChangePassword(UserInformationDTO userInformationDTO)
    {
        var user = _userService.GetUserById(userInformationDTO.UserId);
        if (ModelState.IsValid)
        {
            if (_userService.ChangePassword(userInformationDTO))
            {
                TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                return View("UserInformation", user);
            }
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return View("UserInformation", user);
    }


    [HttpGet]
    public IActionResult DeactiveUser(long userId, long inforId)
    {
        if (_userService.Deactive(userId, inforId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Users");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Users");
    }

    [HttpGet]
    public IActionResult ActiveUser(long userId, long inforId)
    {
        if (_userService.Active(userId, inforId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Users");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Users");
    }

    [HttpPost]
    public IActionResult SearchUser(string searchType, string searchValue)
    {
        if (ModelState.IsValid)
        {
            var searchUsers = _userService.SearchUser(searchType, searchValue);
            if (searchUsers != null)
            {
                return View("Users", searchUsers);
            }
        }
        TempData["Message"] = AppConstant.NOT_FOUND;
        return RedirectToAction("Users");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}