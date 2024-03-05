using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Warehouse.Domain.DTOs.UserDTO;
using Warehouse.Domain.Entities;
using Warehouse.Domain.Interfaces;
using Warehouse.Infrastructure;
using Warehouse.Infrastructure.Utils.Helper;

namespace WarehouseWebMVC.Controllers;

public class AuthenticationController : Controller
{
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IUserService _userService;
    private readonly IAddressHelper _addressHelper;

    public AuthenticationController(ILogger<AuthenticationController> logger,
                                    IUserService userService,
                                    IAddressHelper addressHelper)
    {
        _logger = logger;
        _userService = userService;
        _addressHelper = addressHelper;
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

        var email = HttpContext.Session.GetString("User");
        var addedUserEmail = HttpContext.Session.GetString("AddTokenUserEmail");
        if (email == null || email != addedUserEmail)
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

        Response.Cookies.Delete("rememberMeEmail");
        Response.Cookies.Delete("rememberMePassword");
        Response.Cookies.Delete("rememberMeChecked");

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

        if (newPassword == null || confirmPassword == null)
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
        var userSession = HttpContext.Session.GetString("User");
        var emailSession = HttpContext.Session.GetString("AddTokenUserEmail");

        if (!string.IsNullOrEmpty(userSession) && userSession == userDTO.Email)
        {
            TempData["Message"] = AppConstant.MESSAGE_LOGGED_IN;
            return RedirectToAction("Login");
        }

        if (userSession == null || userSession != null && userSession != userDTO.Email || emailSession != null)
        {
            var loginResult = _userService.CheckLogin(userDTO);

            switch (loginResult)
            {
                case LoginResult.Success:
                    var user = _userService.GetUserByEmail(userDTO.Email);
                    if (user != null)
                    {
                        SetUserSession(user);
                        HandleRememberMeCookie(userDTO);
                        TempData["Message"] = AppConstant.MESSAGE_SUCCESSFUL;
                        return RedirectToAction("Dashboard", "Dashboard");
                    }
                    break;
                case LoginResult.InvalidCredentials:
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    break;
                case LoginResult.AccountLocked:
                    TempData["Message"] = AppConstant.MESSAGE_LOCKED;
                    break;
                default:
                    TempData["Message"] = AppConstant.MESSAGE_FAILED;
                    break;
            }
        }
        return RedirectToAction("Login");
    }

    private void SetUserSession(User user)
    {
        HttpContext.Session.SetString("User", user.Email);
        byte[] userIdBytes = BitConverter.GetBytes(user.UserId);
        HttpContext.Session.Set("Id", userIdBytes);
        HttpContext.Session.SetString("Name", user.Name);
        HttpContext.Session.SetString("Avatar", user.Avatar);
        HttpContext.Session.SetString("Role", user.Role);
        string address = _addressHelper.ExtractCityProvince(user.Address);
        HttpContext.Session.SetString("Address", address);
    }

    private void HandleRememberMeCookie(UserDTO userDTO)
    {
        var rememberMe = Request.Form["remember-me"].Count > 0;
        if (rememberMe)
        {
            var rememberMeCookie = new CookieOptions
            {
                Expires = DateTime.Now.AddDays(10),
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
    }

}
