using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
public class InternalAjaxOnlyAttribute : Attribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;
        var isAjax = request.Headers.XRequestedWith == "XMLHttpRequest";

        var referer = request.Headers.Referer.ToString();
        var host = request.Host.Value;

        var isInternal = !string.IsNullOrEmpty(referer) && referer.Contains(host);

        if (!isAjax || !isInternal)
        {
            context.Result = new ContentResult
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Content = "Forbidden: AJAX internal requests only."
            };
        }
    }
}
