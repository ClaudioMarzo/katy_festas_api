namespace KatyFestas.Domain.EvaluateAttribute;

public static class PasswordHashEvaluateAttribute
{
    public static bool IsValid(string passwordHash)
    {
        return !string.IsNullOrWhiteSpace(passwordHash) && passwordHash.Length == 64;
    }
}