using System.Net;
using System.Text.Json;
using KatyFestas.API.Responses;
using KatyFestas.Domain.Exceptions;

namespace KatyFestas.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            var correlationId = context.Items[CorrelationIdHeader]?.ToString() ?? string.Empty;
            _logger.LogError(ex, "Erro não tratado. CorrelationId: {CorrelationId}", correlationId);
            await HandleExceptionAsync(context, ex, correlationId);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message, errors) = exception switch
        {
            DomainException ex => (HttpStatusCode.BadRequest, ex.Message, new[] { ex.Message }),
            NotFoundException ex => (HttpStatusCode.NotFound, ex.Message, new[] { ex.Message }),
            UnauthorizedException ex => (HttpStatusCode.Unauthorized, ex.Message, new[] { ex.Message }),
            _ => (HttpStatusCode.InternalServerError, "Erro interno do servidor.", Array.Empty<string>())
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponse(message, (int)statusCode, correlationId, errors);
        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

        await context.Response.WriteAsync(json);
    }
}