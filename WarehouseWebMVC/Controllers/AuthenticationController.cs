using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;
using WarehouseWebMVC.Models.Domain;
using WarehouseWebMVC.Models.DTOs;
using WarehouseWebMVC.Service;
using WarehouseWebMVC.Services.Impl;

namespace WarehouseWebMVC.Controllers
{
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
					HttpContext.Session.SetString("User", userDTO.Email.ToString());
					return RedirectToAction("Dashboard", "Dashboard");
				}
			}
			ModelState.AddModelError(string.Empty, "Login fail!!");
			return View("Login");
		}

	}
}
