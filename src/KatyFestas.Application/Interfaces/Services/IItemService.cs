using KatyFestas.Application.Common;
using KatyFestas.Application.DTOs.Item;

namespace KatyFestas.Application.Interfaces.Services;

/// <summary>
/// Interface de serviço para operações de Item
/// </summary>
public interface IItemService
{
    /// <summary>
    /// Busca item por ID
    /// </summary>
    Task<ItemResponseDto> GetByIdAsync(Guid id);

    /// <summary>
    /// Lista todos os items de uma loja com paginação
    /// </summary>
    Task<PagedResponse<ItemResponseDto>> GetByStoreAsync(Guid storeId, PagedRequest request);

    /// <summary>
    /// Cria um novo item
    /// </summary>
    Task<ItemResponseDto> CreateAsync(CreateItemDto dto);

    /// <summary>
    /// Atualiza um item existente
    /// </summary>
    Task<ItemResponseDto> UpdateAsync(Guid id, UpdateItemDto dto);

    /// <summary>
    /// Deleta (soft delete) um item
    /// </summary>
    Task DeleteAsync(Guid id);

    /// <summary>
    /// Ativa um item
    /// </summary>
    Task ActivateAsync(Guid id);

    /// <summary>
    /// Desativa um item
    /// </summary>
    Task DeactivateAsync(Guid id);
}
