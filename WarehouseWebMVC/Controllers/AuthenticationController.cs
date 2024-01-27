﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Data;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs.UserDTO;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services.Impl;

namespace WarehouseWebMVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;

    private readonly IUserService _userService;

    public AuthenticationController(ILogger<AuthenticationController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        var rememberMeEmail = Request.Cookies["rememberMeEmail"] ?? string.Empty;
        var rememberMePassword = Request.Cookies["rememberMePassword"] ?? string.Empty;
        var rememberMeChecked = Request.Cookies["rememberMeChecked"]?.ToLower() == "true";

        ViewBag.RememberMeEmail = rememberMeEmail;
        ViewBag.RememberMePassword = rememberMePassword;
        ViewBag.RememberMeChecked = rememberMeChecked;


        if (HttpContext.Session.GetString("User") == null)
        {
            return View();
        }
        else
        {
            return RedirectToAction("Dashboard", "Dashboard");
        }
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        HttpContext.Session.Remove("User");
        return RedirectToAction("Index", "Home", new { v = DateTime.Now.Ticks });
    }

    [HttpGet]
    public IActionResult ForgotPassword()
    {
       return View();
    }

    [HttpGet]
    public IActionResult ResetPassword()
    {
        return View();
    }

    [HttpPost]
    public IActionResult ResetPassword(UserDTO userDTO)
    {
        var newPassword = userDTO.NewPassword;
        var confirmPassword = userDTO.ConfirmPassword;

        if(newPassword == null ||  confirmPassword == null)
        {
            TempData["Message"] = AppConstant.MESSAGE_NULL;
            return RedirectToAction("ResetPassword", new { token = userDTO.ResetToken });
        }

        if (newPassword != confirmPassword)
        {
            TempData["Message"] = AppConstant.MESSAGE_WRONG_INFO;
            return RedirectToAction("ResetPassword", new { token = userDTO.ResetToken });
        }

        var success = _userService.ResetPassword(userDTO.NewPassword, HttpContext.Session);

        if (success)
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("Login");
        }
        else
        {
            TempData["Message"] = AppConstant.MESSAGE_FAILED;
            return RedirectToAction("ForgotPassword");
        }
    }

    [HttpPost]
    public IActionResult ForgotPassword(UserDTO userDTO)
    {
        var userEmail = userDTO.Email;
        if (userEmail == null)
        {
            TempData["Message"] = AppConstant.MESSAGE_NULL;
            return RedirectToAction("ForgotPassword");
        }

        var sentSuccessfully = _userService.SendResetPasswordEmail(userEmail, HttpContext.Session, HttpContext);

        if (sentSuccessfully)
        {
            TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
            return RedirectToAction("ForgotPassword");
        }
        else
        {
            TempData["Message"] = AppConstant.MESSAGE_WRONG_INFO;
            return RedirectToAction("ForgotPassword");
        }
    }

    [HttpPost]
    public IActionResult Login(UserDTO userDTO)
    {
        if (HttpContext.Session.GetString("User") == null)
        {
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
                        string city = ExtractCityProvince(user.Address);
                        HttpContext.Session.SetString("Address", city);
                        HttpContext.Session.SetString("Avatar", user.Avatar);
                    }
                    else
                    {
                        TempData["Message"] = AppConstant.MESSAGE_FAILED;
                        return RedirectToAction("Login");
                    }

                    var rememberMe = Request.Form["remember-me"].Count > 0;
                    if (rememberMe)
                    {
                        var rememberMeCookie = new CookieOptions
                        {
                            Expires = DateTime.Now.AddDays(30),
                            IsEssential = true,
                            HttpOnly = true,
                            Secure = true,
                            SameSite = SameSiteMode.Unspecified
                        };

                        Response.Cookies.Append("rememberMeEmail", userDTO.Email, rememberMeCookie);
                        Response.Cookies.Append("rememberMePassword", userDTO.Password, rememberMeCookie);
                        Response.Cookies.Append("rememberMeChecked", rememberMe.ToString(), rememberMeCookie);
                    }
                    else
                    {
                        Response.Cookies.Delete("rememberMeEmail");
                        Response.Cookies.Delete("rememberMePassword");
                        Response.Cookies.Delete("rememberMeChecked");
                    }

                    TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                    return RedirectToAction("Dashboard", "Dashboard");

                case LoginResult.InvalidCredentials:
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    return RedirectToAction("Login");

                case LoginResult.AccountLocked:
                    TempData["Message"] = AppConstant.MESSAGE_LOCKED; 
                    return RedirectToAction("Login");

                default:
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    return RedirectToAction("Login");
            }
        }
        return RedirectToAction("Login");
    }

    private static string ExtractCityProvince(string fullAddress)
    {
        string[] addressParts = fullAddress.Split(',');

        if (addressParts.Length >= 4)
        {
            string city = addressParts[3].Trim();
            string province = addressParts[4].Trim();

            return city + ", " + province;
        }

        return null!;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
