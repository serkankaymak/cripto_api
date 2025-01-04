using System.Net;
using System.Text.Json;


namespace api.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHostEnvironment _env;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Sonraki middleware’e geç
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred.");
            await HandleExceptionAsync(context, ex, _env);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception, IHostEnvironment env)
    {
        HttpStatusCode statusCode;
        string message;
        string? details = null;

        switch (exception)
        {
            case ArgumentException argEx:
                statusCode = HttpStatusCode.BadRequest; // 400
                message = argEx.Message;
                break;

            case UnauthorizedAccessException unauthEx:
                statusCode = HttpStatusCode.Unauthorized; // 401
                message = unauthEx.Message;
                break;

            case KeyNotFoundException keyEx:
                statusCode = HttpStatusCode.NotFound; // 404
                message = keyEx.Message;
                break;

            case InvalidOperationException invalidOpEx:
                statusCode = (HttpStatusCode)422; // Unprocessable Entity
                message = invalidOpEx.Message;
                break;

            case ApplicationException appEx:
                statusCode = HttpStatusCode.BadRequest; // 400 veya ihtiyaca göre
                message = appEx.Message;
                break;

            default:
                statusCode = HttpStatusCode.InternalServerError; // 500
                message = "An unexpected error occurred.";
                break;
        }

        if (env.IsDevelopment())
        {
            // Geliştirme ortamında hata detaylarını ekleyebilirsiniz
            details = exception.StackTrace ?? "";
        }

        var response = new
        {
            StatusCode = (int)statusCode,
            Message = message,
            Details = details
        };

        var payload = JsonSerializer.Serialize(response);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        return context.Response.WriteAsync(payload);
    }
}

