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
            return statusCode switch
            {
                404 => View("404"),
                403 => View("403"),
                _ => View("500"),
            };
        }
        else
        {
            return View("500");
        }
    }
}
