namespace KatyFestas.Application.DTOs.Auth;

/// <summary>
/// DTO de resposta de autenticação contendo o token JWT
/// </summary>
public record AuthResponseDto
{
    public string Token { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public Guid StoreId { get; init; }
    public DateTime ExpiresAt { get; init; }
}
