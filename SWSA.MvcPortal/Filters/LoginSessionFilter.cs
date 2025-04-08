using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace SWSA.MvcPortal.Filters;


public class LoginSessionFilter : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (IsAjaxRequest(context.HttpContext))
        {
            await next();
            return;
        }

        if (IsIgnoreLoginSession(context))
        {
            await next();
            return;
        }

        var httpContext = context.HttpContext;
        var session = httpContext.Session;
        var token = session.GetString("StaffId");

        if (IsLoginPage(context) && !string.IsNullOrEmpty(token))
        //If has logged but try to navigate to login page
        {
            RedirectLoginToDashboard(context);
            return;
        }
        if (!string.IsNullOrEmpty(token) || IsLoginPage(context))
        {

            await next();
            return;
        }

        //No refresh token 
        RedirectLoginPage(context);
    }


    private void RedirectLoginPage(ActionExecutingContext context)
    {
        context.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
            { "controller", "Auth" },
            { "action", "Login" },
        });
    }

    private void RedirectLoginToDashboard(ActionExecutingContext context)
    {
        context.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
            { "controller", "Home" },
            { "action", "Dashboard" },
        });
    }

    private bool IsLoginPage(ActionExecutingContext context)
    {
        var routeData = context.RouteData;
        var controller = routeData.Values["controller"]?.ToString();
        var action = routeData.Values["action"]?.ToString();
        return string.Equals(controller, "Auth", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(action, "Login", StringComparison.OrdinalIgnoreCase);
    }

    private bool IsIgnoreLoginSession(ActionExecutingContext context)
    {
        List<(string, string)> IgnoreList = new()
        {
            ("Home", "AccessDenied"),
            ("Home", "NotFound"),
            ("Home", "Privacy"),
            ("Home", "Error"),
        };
        var routeData = context.RouteData;
        var controller = routeData.Values["controller"]?.ToString();
        var action = routeData.Values["action"]?.ToString();

        return IgnoreList.Any(x =>
       x.Item1.Equals(controller, StringComparison.OrdinalIgnoreCase) &&
       x.Item2.Equals(action, StringComparison.OrdinalIgnoreCase));

    }

    private bool IsAjaxRequest(HttpContext httpContext)
    {
        if (httpContext.Request.Headers
            .TryGetValue("X-Requested-With", out Microsoft.Extensions.Primitives.StringValues value) &&
                  value.ToString() == "XMLHttpRequest")
        {
            return true;
        }

        return false;
    }

}