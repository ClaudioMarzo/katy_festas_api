using KatyFestas.Application.DTOs.Category;
using KatyFestas.Application.Interfaces.Services;
using KatyFestas.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KatyFestas.API.Controllers.Public;

/// <summary>
/// Endpoints públicos para consulta de categorias
/// </summary>
[ApiController]
[Route("api/v1/categories")]
[Produces("application/json")]
public class CategoriesController : ControllerBase
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    /// Lista todas as categorias de uma loja
    /// </summary>
    /// <param name="storeId">ID da loja</param>
    /// <returns>Lista de categorias</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CategoryResponseDto>>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll([FromQuery] Guid storeId)
    {
        var categories = await _categoryService.GetByStoreAsync(storeId);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<IEnumerable<CategoryResponseDto>>(
            data: categories,
            message: "Categorias encontradas com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Busca uma categoria por ID
    /// </summary>
    /// <param name="id">ID da categoria</param>
    /// <returns>Dados da categoria</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetByIdAsync(id);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<CategoryResponseDto>(
            data: category,
            message: "Categoria encontrada com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    private string? GetCorrelationId()
    {
        return HttpContext.Items[CorrelationIdHeader]?.ToString();
    }
}
