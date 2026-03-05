namespace KatyFestas.Infrastructure.Security;

public class JwtSettings
{
    public const string SectionName = "Jwt";

    public string SecretKey { get; set; } = string.Empty;
    public int ExpiresInDays { get; set; } = 7;
}
