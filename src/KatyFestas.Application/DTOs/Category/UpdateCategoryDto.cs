namespace KatyFestas.Application.DTOs.Category;

/// <summary>
/// DTO para atualização de Category
/// </summary>
public record UpdateCategoryDto
{
    public string? Name { get; init; }
}
