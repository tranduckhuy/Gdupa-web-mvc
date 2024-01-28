using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services.Helper;
using WarehouseWebMVC.Services.Impl;

namespace WarehouseWebMVC.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserService _userService;
    private readonly IAddressHelper _addressHelper;

    public HomeController(ILogger<HomeController> logger,
                          IUserService userService,
                          IAddressHelper addressHelper)
    {
        _logger = logger;
        _userService = userService;
        _addressHelper = addressHelper;
    }

    public IActionResult Index()
    {
        var rememberMeEmail = Request.Cookies["rememberMeEmail"];
        var rememberMePassword = Request.Cookies["rememberMePassword"];
        var rememberMeChecked = Request.Cookies["rememberMeChecked"];

        if (!string.IsNullOrEmpty(rememberMeEmail) && !string.IsNullOrEmpty(rememberMePassword) && bool.TryParse(rememberMeChecked, out var rememberMe))
        {
            var userDTO = new UserDTO
            {
                Email = rememberMeEmail,
                Password = rememberMePassword
            };

            var loginResult = _userService.CheckLogin(userDTO);

            switch (loginResult)
            {
                case LoginResult.Success:
                    var user = _userService.GetUserByEmail(userDTO.Email);
                    HttpContext.Session.SetString("User", userDTO.Email);
                    if (user != null)
                    {
                        byte[] userIdBytes = BitConverter.GetBytes(user.UserId);
                        HttpContext.Session.Set("Id", userIdBytes);
                        HttpContext.Session.SetString("Name", user.Name);
                        string address = _addressHelper.ExtractCityProvince(user.Address);
                        HttpContext.Session.SetString("Address", address);
                        HttpContext.Session.SetString("Avatar", user.Avatar);
                    }
                    else
                    {
                        TempData["Message"] = AppConstant.MESSAGE_FAILED;
                        return RedirectToAction("Login", "Authentication");
                    }
                    TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                    return RedirectToAction("Dashboard", "Dashboard");

                case LoginResult.InvalidCredentials:
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    return RedirectToAction("Login", "Authentication");

                case LoginResult.AccountLocked:
                    TempData["Message"] = AppConstant.MESSAGE_LOCKED;
                    return RedirectToAction("Login", "Authentication");

                default:
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    return RedirectToAction("Login", "Authentication");
            }
        }
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
