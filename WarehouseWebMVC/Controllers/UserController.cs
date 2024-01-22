using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
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
    public IActionResult UserInformation(UserInformationDTO userInformationDTO)
    {
        if (ModelState.IsValid)
        {
            if (_userService.UpdateUser(userInformationDTO))
            {
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
    public IActionResult DeleteUser(long userId)
    {
        if (_userService.Delete(userId))
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Users");
        }
        TempData["Message"] = AppConstant.MESSAGE_FAILED;
        return RedirectToAction("Users");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}