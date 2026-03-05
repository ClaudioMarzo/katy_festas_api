namespace KatyFestas.Application.DTOs.Auth;

/// <summary>
/// DTO para registro de usuário
/// </summary>
public record RegisterDto
{
    public string Name { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Guid StoreId { get; init; }
}
