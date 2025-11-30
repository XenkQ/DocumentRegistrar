using Backend.Exceptions;

namespace Backend.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (Exception ex)
        {
            var (statusCode, responseBody) = MapException(ex);

            _logger.LogError(ex, responseBody);

            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(responseBody);
        }
    }

    private static (int statusCode, string responseBody) MapException(Exception ex)
    {
        return ex switch
        {
            ForbidException fe => (StatusCodes.Status403Forbidden, fe.Message),
            BadRequestException be => (StatusCodes.Status400BadRequest, be.Message),
            NotFoundException ne => (StatusCodes.Status404NotFound, ne.Message),
            _ => (StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again later.")
        };
    }
}
