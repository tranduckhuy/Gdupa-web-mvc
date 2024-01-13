﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers
{
	public class PaymentController : Controller
	{
		private readonly ILogger<PaymentController> _logger;

		public PaymentController(ILogger<PaymentController> logger)
		{
			_logger = logger;
		}

		public IActionResult Payment()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}