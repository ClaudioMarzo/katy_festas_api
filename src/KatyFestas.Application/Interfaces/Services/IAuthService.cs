using KatyFestas.Application.DTOs.Auth;

namespace KatyFestas.Application.Interfaces.Services;

/// <summary>
/// Interface de serviço para autenticação
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Autentica o usuário e retorna o token JWT
    /// </summary>
    Task<AuthResponseDto> LoginAsync(LoginDto dto);

    /// <summary>
    /// Registra um novo usuário vinculado a uma loja existente
    /// </summary>
    Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
}
