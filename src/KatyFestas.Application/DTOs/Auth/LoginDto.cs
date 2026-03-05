namespace KatyFestas.Application.DTOs.Auth;

/// <summary>
/// DTO para login de usuário
/// </summary>
public record LoginDto
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}
