using KatyFestas.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace KatyFestas.API.Controllers.Admin;

/// <summary>
/// Controller base para endpoints administrativos.
/// Fornece acesso ao StoreId do usuário autenticado via JWT claims.
/// Em ambiente Development, aceita o header X-Store-Id para facilitar testes.
/// </summary>
public abstract class AdminBaseController : ControllerBase
{
    protected const string CorrelationIdHeader = "X-Correlation-Id";

    /// <summary>
    /// Obtém o StoreId do usuário autenticado via claim do JWT.
    /// Em Development, aceita o header X-Store-Id como fallback.
    /// </summary>
    protected Guid GetStoreId()
    {
        // 1. Tenta extrair do claim do JWT
        var storeIdClaim = User.FindFirst("store_id")?.Value;
        if (Guid.TryParse(storeIdClaim, out var storeId))
            return storeId;

        // 2. Em Development, aceita header X-Store-Id para facilitar testes
        var env = HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>();
        if (env.IsDevelopment())
        {
            var headerValue = HttpContext.Request.Headers["X-Store-Id"].FirstOrDefault();
            if (Guid.TryParse(headerValue, out var headerStoreId))
                return headerStoreId;
        }

        throw new UnauthorizedException("StoreId não encontrado no token de autenticação.");
    }

    protected string? GetCorrelationId()
    {
        return HttpContext.Items[CorrelationIdHeader]?.ToString();
    }
}
