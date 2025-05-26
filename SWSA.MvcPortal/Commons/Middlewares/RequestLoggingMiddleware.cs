using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Context;
using SWSA.MvcPortal.Services.Interfaces.SystemCore;
using System.Diagnostics;
using System.Text;

namespace SWSA.MvcPortal.Commons.Middlewares;

public class RequestLoggingMiddleware
{

    private readonly RequestDelegate _next;
    private const int MaxBody = 2048;
    private static readonly string[] StaticExt =
    { ".css",".js",".jpg",".jpeg",".png",".gif",".svg",".woff",".woff2",".eot",".ttf",".otf",".ico" };

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {

        //Skip static resources
        if (StaticExt.Any(e => context.Request.Path.Value!.EndsWith(e, StringComparison.OrdinalIgnoreCase)))
        {
            await _next(context);
            return;
        }

        var sw = Stopwatch.StartNew();
        var traceId = Activity.Current?.TraceId.ToString() ?? Guid.NewGuid().ToString();
        var route = context.GetEndpoint()?.Metadata.GetMetadata<RouteAttribute>()?.Template
                    ?? context.Request.Path.Value ?? "/";
        var method = context.Request.Method;
        var query = context.Request.QueryString.ToString();
        var requestBytes = context.Request.ContentLength ?? 0;
        var responseBytes = 0L;
        var statusCode = 200;
        var body = await ReadBodyAsync(context);

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

        try
        {
            await _next(context);
            statusCode = context.Response.StatusCode;
            responseBytes = context.Response.ContentLength ?? 0;
        }
        finally
        {
            sw.Stop();
            using (LogContext.PushProperty("LogType", "Request"))
            using (LogContext.PushProperty("TraceId", traceId))
            using (LogContext.PushProperty("Route", route))
            using (LogContext.PushProperty("Method", method))
            using (LogContext.PushProperty("Query", query))
            using (LogContext.PushProperty("RequestBody", SanitizeBody(body)))
            using (LogContext.PushProperty("UserName", userName))
            using (LogContext.PushProperty("StaffId", staffId))
            using (LogContext.PushProperty("RemoteIP", context.Connection.RemoteIpAddress?.ToString()))
            using (LogContext.PushProperty("UserAgent", context.Request.Headers.UserAgent.ToString()))
            {
                Log.Information("Request completed: {Method} {Route} => {StatusCode} in {Duration}ms | In:{RequestBytes}b Out:{ResponseBytes}b",
                    method, route, statusCode, sw.ElapsedMilliseconds, requestBytes, responseBytes);
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

    private static async Task<string> ReadBodyAsync(HttpContext ctx)
    {
        if (!(ctx.Request.Method == HttpMethods.Post || ctx.Request.Method == HttpMethods.Put))
            return string.Empty;

        ctx.Request.EnableBuffering();
        using var sr = new StreamReader(ctx.Request.Body, Encoding.UTF8, leaveOpen: true);
        var text = await sr.ReadToEndAsync();
        ctx.Request.Body.Position = 0;
        return text.Length <= MaxBody ? text : text[..MaxBody] + "...(truncated)";
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLogging(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestLoggingMiddleware>();
    }
}