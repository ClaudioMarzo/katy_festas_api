namespace KatyFestas.Application.DTOs.Item;

/// <summary>
/// DTO de resposta para Item
/// </summary>
public record ItemResponseDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public decimal Price { get; init; }
    public int StockQuantity { get; init; }
    public bool IsActive { get; init; }
    public Guid StoreId { get; init; }
    public string StoreName { get; init; } = string.Empty;
    public Guid CategoryId { get; init; }
    public string CategoryName { get; init; } = string.Empty;
    public List<ItemPhotoDto> Photos { get; init; } = [];
    public DateTime CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}

/// <summary>
/// DTO para fotos do item
/// </summary>
public record ItemPhotoDto
{
    public Guid Id { get; init; }
    public string Url { get; init; } = string.Empty;
    public int Order { get; init; }
}
