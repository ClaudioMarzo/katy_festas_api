namespace KatyFestas.API.Middlewares;

/// <summary>
/// Middleware para adicionar CorrelationId único em cada requisição
/// </summary>
public class CorrelationIdMiddleware
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly RequestDelegate _next;

    public CorrelationIdMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Gera ou usa CorrelationId existente do header
        var correlationId = context.Request.Headers[CorrelationIdHeader].FirstOrDefault()
            ?? Guid.NewGuid().ToString();

        // Armazena no HttpContext.Items para uso nos controllers e middlewares
        context.Items[CorrelationIdHeader] = correlationId;

        // Adiciona no response header
        context.Response.Headers[CorrelationIdHeader] = correlationId;

        await _next(context);
    }
}
