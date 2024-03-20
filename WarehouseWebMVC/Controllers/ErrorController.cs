using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using System.Diagnostics;

namespace WarehouseWebMVC.Controllers;

public class ErrorController : Controller
{
    [Route("/Error/Error/{statusCode}")]
    public IActionResult Error(int? statusCode)
    {
        try
        {
            if (statusCode.HasValue)
            {
                return statusCode switch
                {
                    404 => View("404"),
                    403 => View("403"),
                    500 => View("500"),
                    _ => View("500")
                };
            }
            else
            {
                return View("500");
            }
        }
        catch (SqliteException)
        {
            return View("500");
        }
        catch (Exception)
        {
            return View("500");
        }
    }
}
