using Serilog;
using Serilog.Context;
using SWSA.MvcPortal.Services.Interfaces;
using System.Text;

namespace SWSA.MvcPortal.Commons.Middlewares;

public class RequestLoggingMiddleware
{

    private readonly RequestDelegate _next;
    private const int MaxBodyLength = 1000;
    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {   
        var path = context.Request.Path;
        var method = context.Request.Method;
        var query = context.Request.QueryString.ToString();

        string body = "";

        IUserContext? userContext = null;
        try
        {
            userContext = context.RequestServices.GetService<IUserContext>();
        }
        catch
        {
            // 如果注入失败，则保持为 null
        }

        var userName = userContext?.Name ?? "System / Anonymous";
        var staffId = userContext?.StaffId ?? "-";
        var companyId = userContext?.CompanyId?.ToString() ?? "-";
        var companyDeptId = userContext?.CompanyDepartmentId?.ToString() ?? "-";

        using (LogContext.PushProperty("UserName", userName))
        using (LogContext.PushProperty("StaffId", staffId))
        using (LogContext.PushProperty("CompanyId", companyId))
        using (LogContext.PushProperty("CompanyDepartmentId", companyDeptId))
        using (LogContext.PushProperty("Path", path))
        using (LogContext.PushProperty("Method", method))
        using (LogContext.PushProperty("Query", query))
        using (LogContext.PushProperty("RequestBody", body))
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
                {
                    context.Request.EnableBuffering();
                    using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true);
                    body = await reader.ReadToEndAsync();
                    context.Request.Body.Position = 0;

                    if (body.Length > MaxBodyLength)
                        body = body.Substring(0, MaxBodyLength) + "...(truncated)";
                }

                // 清理敏感信息
                body = SanitizeBody(body);
                Log.Error(ex, "Unhandled exception occurred.");
                throw;
            }
        }
    }

    private string SanitizeBody(string body)
    {
        // 可以根据需求加入更复杂的敏感字段过滤
        var sensitiveFields = new[] { "password", "ssn", "creditCard" };

        foreach (var field in sensitiveFields)
        {
            if (body.Contains(field, StringComparison.OrdinalIgnoreCase))
            {
                body = body.Replace(field, "***", StringComparison.OrdinalIgnoreCase);
            }
        }

        return body;
    }
}



public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}