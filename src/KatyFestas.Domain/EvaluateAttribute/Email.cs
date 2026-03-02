using System.Text.RegularExpressions;

namespace KatyFestas.Domain.EvaluateAttribute;

public static class EmailEvaluateAttribute
{   
    private static readonly Regex EmailRegex = new(
        @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
        RegexOptions.Compiled | RegexOptions.IgnoreCase,
        TimeSpan.FromMilliseconds(250)
    );

    public static bool IsValid(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        if (value.Length > 254) // RFC 5321
            return false;

        return EmailRegex.IsMatch(value);
    }
}