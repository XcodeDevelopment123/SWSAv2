using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SWSA.MvcPortal.Commons.Constants;
using SWSA.MvcPortal.Commons.Services.Session;
using SWSA.MvcPortal.Commons.Services.Permission;
using SWSA.MvcPortal.Services.Interfaces.SystemCore;

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

        //If this user require to refresh session to update permission / other information
        var result =await TryRefreshSession(httpContext, userContext) ;
        if (!result) //User are require, but find user by staff id failed
        {
            RedirectLoginPage(context);
            return;
        }
    
        var loginTimeStr = session.GetString(SessionKeys.LoginTime);

        if (DateTime.TryParse(loginTimeStr, out var loginTime))
        {
            //early refresh session, prevent token expired
            if ((DateTime.Now - loginTime).TotalMinutes > 20)
            {
                var sessionWriter = serviceProvider.GetRequiredService<IUserSessionWriter>();
                var userFetcher = serviceProvider.GetRequiredService<IUserFetcher>();
                var user = await userFetcher.GetByStaffId(userContext.StaffId);
                if(user == null)
                {
                    RedirectLoginPage(context);
                    return;
                }
                sessionWriter.Write(user!);
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
            ("Errors", "AccessDenied"),
            ("Errors", "NotFound"),
            ("Errors", "Privacy"),
            ("Errors", "Error"),
            ("Errors", "ServerError"),
            ("Auth", "KeepAlive")
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

    private async Task<bool> TryRefreshSession(HttpContext httpContext, IUserContext userContext)
    {
        var refreshTracker = httpContext.RequestServices.GetRequiredService<IPermissionRefreshTracker>();
        if (!refreshTracker.IsRefreshNeeded(userContext.StaffId)) return true;

        var userFetcher = httpContext.RequestServices.GetRequiredService<IUserFetcher>();
        var sessionWriter = httpContext.RequestServices.GetRequiredService<IUserSessionWriter>();
        var user = await userFetcher.GetByStaffId(userContext.StaffId);

        if (user == null) return false;

        sessionWriter.Write(user);
        refreshTracker.Clear(userContext.StaffId);
        return true;
    }

}