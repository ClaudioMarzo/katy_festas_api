using KatyFestas.Application.DTOs.Category;

namespace KatyFestas.Application.Interfaces.Services;

/// <summary>
/// Interface de serviço para operações de Category
/// </summary>
public interface ICategoryService
{
    /// <summary>
    /// Busca categoria por ID
    /// </summary>
    Task<CategoryResponseDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Lista todas as categorias de uma loja
    /// </summary>
    Task<IEnumerable<CategoryResponseDto>> GetByStoreAsync(Guid storeId);

    /// <summary>
    /// Cria uma nova categoria
    /// </summary>
    Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto);

    /// <summary>
    /// Atualiza uma categoria existente
    /// </summary>
    Task<CategoryResponseDto> UpdateAsync(Guid id, UpdateCategoryDto dto);

    /// <summary>
    /// Deleta (soft delete) uma categoria
    /// </summary>
    Task DeleteAsync(Guid id);
}
