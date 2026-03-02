namespace KatyFestas.Application.DTOs.Category;

/// <summary>
/// DTO para criação de Category
/// </summary>
public record CreateCategoryDto
{
    public Guid StoreId { get; init; }
    public string Name { get; init; } = string.Empty;
}
