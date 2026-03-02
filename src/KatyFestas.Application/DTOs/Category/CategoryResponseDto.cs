namespace KatyFestas.Application.DTOs.Category;

/// <summary>
/// DTO de resposta para Category
/// </summary>
public record CategoryResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public Guid StoreId { get; init; }
    public string StoreName { get; init; } = string.Empty;
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
