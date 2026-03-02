namespace KatyFestas.Application.DTOs.Item;

/// <summary>
/// DTO para atualização de Item
/// </summary>
public record UpdateItemDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public decimal? Price { get; init; }
    public int? StockQuantity { get; init; }
}
