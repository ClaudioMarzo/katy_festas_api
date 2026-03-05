using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using KatyFestas.Domain.Interfaces.Services;

namespace KatyFestas.Infrastructure.Security;

public class TokenService : ITokenService
{
    private readonly string _secretKey;

    public int ExpiresInDays { get; }

    public TokenService(string secretKey, int expiresInDays)
    {
        _secretKey = secretKey;
        ExpiresInDays = expiresInDays;
    }

    public string GenerateToken(Guid userId, Guid storeId, string userName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("store_id", storeId.ToString()),
            new Claim("name", userName)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(ExpiresInDays),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
