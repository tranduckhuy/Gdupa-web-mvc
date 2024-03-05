using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

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
                case 403:
                    return View("403");
                default:
                    return View("500");
            }
        }
        else
        {
            return View("500");
        }
    }
}
