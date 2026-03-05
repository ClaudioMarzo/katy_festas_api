using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Exceptions;
using KatyFestas.Domain.Interfaces;
using KatyFestas.Domain.Interfaces.Services;
using KatyFestas.Application.DTOs.Auth;
using KatyFestas.Application.Interfaces.Services;

namespace KatyFestas.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITokenService _tokenService;
    private readonly IEncryptionService _encryptionService;

    public AuthService(IUnitOfWork unitOfWork, ITokenService tokenService, IEncryptionService encryptionService)
    {
        _unitOfWork = unitOfWork;
        _tokenService = tokenService;
        _encryptionService = encryptionService;
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _unitOfWork.Users.GetByEmailAsync(dto.Email)
            ?? throw new UnauthorizedException("Email ou senha inválidos.");

        if (!user.IsActive)
            throw new UnauthorizedException("Usuário está desativado.");

        if (!user.VerifyPassword(dto.Password, _encryptionService))
            throw new UnauthorizedException("Email ou senha inválidos.");

        var token = _tokenService.GenerateToken(user.Id, user.StoreId, user.Name);

        return new AuthResponseDto
        {
            Token = token,
            UserName = user.Name,
            Email = user.Email,
            StoreId = user.StoreId,
            ExpiresAt = DateTime.UtcNow.AddDays(_tokenService.ExpiresInDays)
        };
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
    {
        var emailExists = await _unitOfWork.Users.EmailExistsAsync(dto.Email);
        if (emailExists)
            throw new DomainException("Este email já está em uso.");

        var store = await _unitOfWork.Stores.GetByIdAsync(dto.StoreId)
            ?? throw new NotFoundException($"Loja com ID {dto.StoreId} não encontrada.");

        var user = User.Create(dto.StoreId, dto.Name, dto.Email, dto.Password, _encryptionService);

        await _unitOfWork.Users.AddAsync(user);
        await _unitOfWork.CommitAsync();

        var token = _tokenService.GenerateToken(user.Id, user.StoreId, user.Name);

        return new AuthResponseDto
        {
            Token = token,
            UserName = user.Name,
            Email = user.Email,
            StoreId = user.StoreId,
            ExpiresAt = DateTime.UtcNow.AddDays(_tokenService.ExpiresInDays)
        };
    }
}
