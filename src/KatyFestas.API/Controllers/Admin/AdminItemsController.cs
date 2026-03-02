using KatyFestas.Application.Common;
using KatyFestas.Application.DTOs.Item;
using KatyFestas.Application.Interfaces.Services;
using KatyFestas.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KatyFestas.API.Controllers.Admin;

/// <summary>
/// Endpoints administrativos para gerenciamento de itens
/// </summary>
[ApiController]
[Route("api/v1/admin/items")]
[Produces("application/json")]
// [Authorize(Roles = "Admin")] // Descomentar quando implementar autenticação
public class AdminItemsController : ControllerBase
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly IItemService _itemService;

    public AdminItemsController(IItemService itemService)
    {
        _itemService = itemService;
    }

    /// <summary>
    /// Lista todos os itens de uma loja com paginação
    /// </summary>
    /// <param name="storeId">ID da loja</param>
    /// <param name="page">Número da página (padrão: 1)</param>
    /// <param name="pageSize">Tamanho da página (padrão: 20, máximo: 100)</param>
    /// <returns>Lista paginada de itens</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PagedApiResponse<ItemResponseDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] Guid storeId, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
    {
        var request = new PagedRequest { Page = page, PageSize = pageSize };
        var result = await _itemService.GetByStoreAsync(storeId, request);
        var correlationId = GetCorrelationId();

        var response = new PagedApiResponse<ItemResponseDto>(
            data: result.Data,
            page: result.Page,
            pageSize: result.PageSize,
            totalItems: result.TotalItems,
            totalPages: result.TotalPages,
            message: "Itens encontrados com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Busca um item por ID
    /// </summary>
    /// <param name="id">ID do item</param>
    /// <returns>Dados do item</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _itemService.GetByIdAsync(id);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<ItemResponseDto>(
            data: item,
            message: "Item encontrado com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Cria um novo item
    /// </summary>
    /// <param name="dto">Dados do item</param>
    /// <returns>Item criado</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<ItemResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateItemDto dto)
    {
        var item = await _itemService.CreateAsync(dto);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<ItemResponseDto>(
            data: item,
            message: "Item criado com sucesso",
            correlationId: correlationId
        );

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = item.Id },
            value: response
        );
    }

    /// <summary>
    /// Atualiza um item existente
    /// </summary>
    /// <param name="id">ID do item</param>
    /// <param name="dto">Dados para atualização</param>
    /// <returns>Item atualizado</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<ItemResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateItemDto dto)
    {
        var item = await _itemService.UpdateAsync(id, dto);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<ItemResponseDto>(
            data: item,
            message: "Item atualizado com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Deleta (soft delete) um item
    /// </summary>
    /// <param name="id">ID do item</param>
    /// <returns>Confirmação de exclusão</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _itemService.DeleteAsync(id);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<object?>(
            data: null,
            message: "Item deletado com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Ativa um item
    /// </summary>
    /// <param name="id">ID do item</param>
    /// <returns>Confirmação de ativação</returns>
    [HttpPatch("{id:guid}/activate")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Activate(Guid id)
    {
        await _itemService.ActivateAsync(id);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<object?>(
            data: null,
            message: "Item ativado com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Desativa um item
    /// </summary>
    /// <param name="id">ID do item</param>
    /// <returns>Confirmação de desativação</returns>
    [HttpPatch("{id:guid}/deactivate")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Deactivate(Guid id)
    {
        await _itemService.DeactivateAsync(id);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<object?>(
            data: null,
            message: "Item desativado com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    private string? GetCorrelationId()
    {
        return HttpContext.Items[CorrelationIdHeader]?.ToString();
    }
}
