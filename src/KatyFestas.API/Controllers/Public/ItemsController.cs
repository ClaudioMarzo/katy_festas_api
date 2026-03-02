using KatyFestas.Application.Common;
using KatyFestas.Application.DTOs.Item;
using KatyFestas.Application.Interfaces.Services;
using KatyFestas.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KatyFestas.API.Controllers.Public;

/// <summary>
/// Endpoints públicos para consulta de itens
/// </summary>
[ApiController]
[Route("api/v1/items")]
[Produces("application/json")]
public class ItemsController : ControllerBase
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly IItemService _itemService;

    public ItemsController(IItemService itemService)
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

    private string? GetCorrelationId()
    {
        return HttpContext.Items[CorrelationIdHeader]?.ToString();
    }
}
