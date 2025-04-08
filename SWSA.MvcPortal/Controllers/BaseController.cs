using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace SWSA.MvcPortal.Controllers;

public class BaseController : Controller
{
    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        ViewData["controller"] = filterContext.RouteData.Values["controller"];
        ViewData["action"] = filterContext.RouteData.Values["action"];

        base.OnActionExecuting(filterContext);
    }
}
