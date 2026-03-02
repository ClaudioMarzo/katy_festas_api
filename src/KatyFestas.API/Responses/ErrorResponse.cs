namespace KatyFestas.API.Responses;

/// <summary>
/// Response padronizado de erro
/// </summary>
public class ErrorResponse
{
    public bool Success { get; set; } = false;
    public string Message { get; set; } = string.Empty;
    public string? CorrelationId { get; set; }
    public int StatusCode { get; set; }
    public IEnumerable<string> Errors { get; set; } = [];

    public ErrorResponse() { }

    public ErrorResponse(string message, int statusCode, string? correlationId = null, IEnumerable<string>? errors = null)
    {
        Message = message;
        StatusCode = statusCode;
        CorrelationId = correlationId;
        Errors = errors ?? [];
    }
}
