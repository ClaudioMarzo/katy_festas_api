using KatyFestas.Application.DTOs.Auth;
using KatyFestas.Application.Interfaces.Services;
using KatyFestas.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KatyFestas.API.Controllers.Public;

/// <summary>
/// Endpoints de autenticação
/// </summary>
[ApiController]
[Route("api/v1/auth")]
[Produces("application/json")]
public class AuthController : ControllerBase
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Autentica um usuário e retorna o token JWT
    /// </summary>
    /// <param name="dto">Credenciais do usuário</param>
    /// <returns>Token JWT e dados do usuário</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<AuthResponseDto>(
            data: result,
            message: "Login realizado com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Registra um novo usuário vinculado a uma loja existente
    /// </summary>
    /// <param name="dto">Dados do novo usuário</param>
    /// <returns>Token JWT e dados do usuário criado</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(ApiResponse<AuthResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<AuthResponseDto>(
            data: result,
            message: "Usuário registrado com sucesso",
            correlationId: correlationId
        );

        return StatusCode(StatusCodes.Status201Created, response);
    }

    private string? GetCorrelationId()
    {
        return HttpContext.Items[CorrelationIdHeader]?.ToString();
    }
}
