using System.Net;
using System.Text.Json;
using FluentValidation;

namespace WebApi.Middleware;

public class ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, responseBody) = exception switch
        {
            ValidationException valEx => (HttpStatusCode.BadRequest,
                CreateResponse("ValidationError", "One or more validation errors occurred.",
                    valEx.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }))),

            KeyNotFoundException => (HttpStatusCode.NotFound, CreateResponse("NotFound", exception.Message)),

            UnauthorizedAccessException => (HttpStatusCode.Unauthorized,
                CreateResponse("Unauthorized", "Access denied.")),

            _ => (HttpStatusCode.InternalServerError, CreateResponse(exception.GetType().Name, exception.Message))
        };

        if (statusCode == HttpStatusCode.InternalServerError)
            logger.LogError(exception, "Unhandled exception: {Message}", exception.Message);
        else
            logger.LogWarning("Request error: {Message}. Path: {Path}", exception.Message, context.Request.Path);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(responseBody));
    }

    private static object CreateResponse(string type, string message, object? errors = null)
        => new { Type = type, Message = message, Errors = errors };
}