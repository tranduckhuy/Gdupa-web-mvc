using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WarehouseWebMVC.Models;

namespace WarehouseWebMVC.Controllers;

public class ErrorController : Controller
{
    [Route("/Error/Error/{statusCode}")]
    public IActionResult Error(int? statusCode)
    {
        if (statusCode.HasValue)
        {
            switch (statusCode)
            {
                case 404:
                    return View("404");
                case 500:
                    return View("500");
                case 403:
                    return View("403");
                default:
                    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
        }
        else
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
