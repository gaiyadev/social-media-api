using System.Net;
using Newtonsoft.Json;
using SocialMediaApp.Responses;
using SocialMediaApp.Utils;

namespace SocialMediaApp.Middlewares;

public sealed class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Execute the next middleware
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred.");

            // Handle the exception and return an error response with details
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";

            var errorResponse = new ErrorResponseModel
            {
                StatusCode = context.Response.StatusCode,
                Error =  HttpStatusTitles.InternalServerError,
                Message = ex.Message,
                StackTrace = ex.StackTrace,
            };
            await context.Response.WriteAsync(JsonConvert.SerializeObject(errorResponse));
        }
    }
}