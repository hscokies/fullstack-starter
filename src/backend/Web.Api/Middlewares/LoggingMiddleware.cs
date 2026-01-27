using System.Diagnostics;
using NLog;

namespace Web.Api.Middlewares;

internal sealed class LoggingMiddleware(RequestDelegate next)
{
    private readonly Logger _logger = LogManager.GetCurrentClassLogger();

    public async Task Invoke(HttpContext httpContext)
    {
        var request = httpContext.Request;
        
        _logger
            .WithProperty("UserAgent", httpContext.Request.Headers.UserAgent)
            .Info("[{traceId}] {method}: {path}",
                Activity.Current?.TraceId.ToString() ?? httpContext.TraceIdentifier,
                request.Method,
                $"{request.Path}{request.QueryString}");

        await next.Invoke(httpContext);
    }
}
