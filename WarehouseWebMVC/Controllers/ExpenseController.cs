using Microsoft.AspNetCore.Mvc;
using WarehouseWebMVC.AuthenticationFilter;

namespace WarehouseWebMVC.Controllers;

public class ExpenseController : Controller
{
    private readonly ILogger<ExpenseController> _logger;

    public ExpenseController(ILogger<ExpenseController> logger)
    {
        _logger = logger;
    }

    [Filter]
    public IActionResult Expense()
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            return View();
        }
        return RedirectToAction("Login", "Authentication");
    }

    [Filter]
    public IActionResult AddExpense()
    {
        if (HttpContext.Session.GetString("User") != null)
        {
            Response.Headers.Add("Cache-Control", "no-store, no-cache, must-revalidate, post-check=0, pre-check=0");
            Response.Headers.Add("Pragma", "no-cache");
            Response.Headers.Add("Expires", "0");
            return View();
        }
        return RedirectToAction("Login", "Authentication");
    }

}
