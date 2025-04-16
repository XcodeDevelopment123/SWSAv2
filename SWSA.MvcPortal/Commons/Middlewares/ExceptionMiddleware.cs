
using Microsoft.AspNetCore.Http.HttpResults;
using SWSA.MvcPortal.Commons.Exceptions;

namespace SWSA.MvcPortal.Commons.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        var isAjax = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

        switch (ex)
        {
            case DataNotFoundException notFoundEx:
                if (isAjax)
                    await WriteJsonError(context, StatusCodes.Status404NotFound, "Data not found", notFoundEx);
                else
                    RedirectToPage(context, "/errors/NotFound");
                break;

            case UnauthorizedAccessException unauthorizedEx:
                if (isAjax)
                    await WriteJsonError(context, StatusCodes.Status403Forbidden, "Unauthorized access", unauthorizedEx);
                else
                    RedirectToPage(context, "/errors/Forbidden");
                break;

            case BusinessLogicException businessEx:
                if (isAjax)
                    await WriteJsonError(context, StatusCodes.Status400BadRequest, "Operation not permitted by current business rules.", businessEx);
                  else
                    RedirectToPage(context, $"/errors/BusinessError?message={Uri.EscapeDataString(businessEx.Message)}");
                break;

            default:
                if (isAjax)
                    await WriteJsonError(context, StatusCodes.Status500InternalServerError, "An unexpected error occurred", ex);
                else
                    RedirectToPage(context, "/errors/ServerError");
                break;
        }
    }

    private async Task WriteJsonError(HttpContext context, int statusCode, string message, Exception ex)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var error = new
        {
            success = false,
            message,
            error = ex.Message
        };

        await context.Response.WriteAsJsonAsync(error);
    }

    private void RedirectToPage(HttpContext context, string path)
    {
        context.Response.Redirect(path);
    }

}
