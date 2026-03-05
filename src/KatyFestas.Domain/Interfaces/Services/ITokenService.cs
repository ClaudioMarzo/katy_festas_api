namespace KatyFestas.Domain.Interfaces.Services;

/// <summary>
/// Interface para geração de tokens JWT
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Dias até o token expirar
    /// </summary>
    int ExpiresInDays { get; }

    /// <summary>
    /// Gera um token JWT para o usuário
    /// </summary>
    string GenerateToken(Guid userId, Guid storeId, string userName);
}
