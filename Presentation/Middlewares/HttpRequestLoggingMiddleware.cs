using System.Security.Claims;

namespace Playmor_Asp.Presentation.Middlewares;

public class HttpRequestLoggingMiddleware : IMiddleware
{
    private readonly ILogger<HttpRequestLoggingMiddleware> _logger;

    public HttpRequestLoggingMiddleware(ILogger<HttpRequestLoggingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        var username = GetMyUsername(httpContext);
        _logger.LogInformation($"Request: {httpContext.Request.Method} {httpContext.Request.Path} - User: {username}");

        await next(httpContext); // Invoke the next middleware
    }

    private static string GetMyUsername(HttpContext httpContext)
    {
        var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (userId == null)
            return "Guest";

        var userName = httpContext.User.FindFirst(ClaimTypes.Name)?.Value;
        return $"name: {userName} | id: {userId?.ToString()}";
    }
}
