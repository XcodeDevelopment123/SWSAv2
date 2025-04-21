using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Services.Interfaces;

namespace SWSA.MvcPortal.Commons.Filters;

public class LoginSessionFilter(
    IServiceProvider serviceProvider
) : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (IsAjaxRequest(context.HttpContext) || IsIgnoreLoginSession(context))
        {
            await next();
            return;
        }

        var httpContext = context.HttpContext;
        var session = httpContext.Session;
        var userContext = serviceProvider.GetRequiredService<IUserContext>();

        //If has logged but try to navigate to login page
        if (IsLoginPage(context))
        {
            if (!string.IsNullOrEmpty(userContext.StaffId))
            {
                RedirectLoginToHome(context);
                return;
            }
            //No Logged
            await next();
            return;
        }

        if (string.IsNullOrEmpty(userContext.StaffId))
        {
            RedirectLoginPage(context);
            return;
        }

        var loginTimeStr = session.GetString(SessionKeys.LoginTime);

        if (DateTime.TryParse(loginTimeStr, out var loginTime))
        {
            //
            if ((DateTime.Now - loginTime).TotalMinutes > 20)
            {
                if (userContext.IsCompanyStaff)
                {
                    var staffService = serviceProvider.GetRequiredService<ICompanyStaffService>();
                    await staffService.SetStaffSession(userContext.StaffId);
                }
                else
                {
                    var userService = serviceProvider.GetRequiredService<IUserService>();
                    await userService.SetUserSession(userContext.StaffId);
                }
            }
        }
        await next();
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
            { "controller", "Dashboard" },
            { "action", "Overall" },
        });
    }

    private void RedirectLoginToHome(ActionExecutingContext context)
    {
        context.Result = new RedirectToRouteResult(new RouteValueDictionary
        {
            { "controller", "Home" },
            { "action", "Home" },
        });
    }

    private bool IsLoginPage(ActionExecutingContext context)
    {
        var routeData = context.RouteData;
        var controller = routeData.Values["controller"]?.ToString();
        var action = routeData.Values["action"]?.ToString();
        return string.Equals(controller, "Auth", StringComparison.OrdinalIgnoreCase) &&
                (string.Equals(action, "Login", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(action, "PartnerLogin", StringComparison.OrdinalIgnoreCase)
                );
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