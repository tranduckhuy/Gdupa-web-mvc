using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WarehouseWebMVC.AuthenticationFilter;

public class Filter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.RouteData.Values["controller"]?.ToString();
        if (controller == "User")
        {
            var role = context.HttpContext.Session.GetString("Role");
            if (role == "Staff")
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
            {
                {"Controller", "Error"},
                {"Action", "Error"},
                {"statusCode", 403}
            });
                return;
            }
        }

        var user = context.HttpContext.Session.GetString("User");
        if (user == null)
        {
            context.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
            {"Controller", "Authentication"},
            {"Action", "Login"}
        });
            return;
        }
    }
}
