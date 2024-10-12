using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Playmor_Asp.Presentation.Middlewares;

public class GlobalExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        try
        {
            await next(httpContext); // Call the next middleware in the pipeline
        }
        catch (ArgumentException ex)
        {
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status400BadRequest, "Bad Request"); // 400
        }
        catch (UnauthorizedAccessException ex)
        {
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status401Unauthorized, "Unauthorized"); // 401
        }
        catch (InvalidOperationException ex)
        {
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status404NotFound, "Not Found"); // 404
        }
        catch (ValidationException ex)
        {
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status422UnprocessableEntity, "Unprocessable Entity"); // 422
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex, StatusCodes.Status500InternalServerError, "Internal Server Error"); // 500
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception, int statusCode, string title)
    {
        _logger.LogError(exception, $"{this.GetType().Name} encountered an error: {title}");

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var result = JsonSerializer.Serialize(new
        {
            context.Response.StatusCode,
            Message = title
        });

        await context.Response.WriteAsync(result);
    }
}
