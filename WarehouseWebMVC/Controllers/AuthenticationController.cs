using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Service;

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

    public IActionResult ForgotPassword()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public IActionResult Login(UserDTO userDTO)
    {
        if (HttpContext.Session.GetString("User") == null)
        {
            var loginSuccess = _userService.CheckLogin(userDTO);
            if (loginSuccess)
            {
                var user = _userService.GetUserByEmail(userDTO);
                HttpContext.Session.SetString("User", userDTO.Email.ToString());
                HttpContext.Session.SetString("Name", user.Name.ToString());
                HttpContext.Session.SetString("Address", user.Address.ToString());
                var rememberMe = Request.Form["remember-me"].Count > 0;
                if (rememberMe)
                {
                    var rememberMeCookie = new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30),
                        IsEssential = true
                    };


                    Response.Cookies.Append("rememberMeEmail", userDTO.Email.ToString(), rememberMeCookie);
                    Response.Cookies.Append("rememberMePassword", userDTO.Password.ToString(), rememberMeCookie);
                    Response.Cookies.Append("rememberMeChecked", rememberMe.ToString(), rememberMeCookie);
                }
                else
                {
                    Response.Cookies.Delete("rememberMeEmail");
                    Response.Cookies.Delete("rememberMePassword");
                    Response.Cookies.Delete("rememberMeChecked");
                }
                return RedirectToAction("Dashboard", "Dashboard");
            }
        }
        ModelState.AddModelError(string.Empty, "Login fail!!");
        return View("Login");
    }

}
