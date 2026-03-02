using KatyFestas.Application.DTOs.Category;
using KatyFestas.Application.Interfaces.Services;
using KatyFestas.API.Responses;
using Microsoft.AspNetCore.Mvc;

namespace KatyFestas.API.Controllers.Admin;

/// <summary>
/// Endpoints administrativos para gerenciamento de categorias
/// </summary>
[ApiController]
[Route("api/v1/admin/categories")]
[Produces("application/json")]
// [Authorize(Roles = "Admin")] // Descomentar quando implementar autenticação
public class AdminCategoriesController : ControllerBase
{
    private const string CorrelationIdHeader = "X-Correlation-Id";
    private readonly ICategoryService _categoryService;

    public AdminCategoriesController(ICategoryService categoryService)
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

    /// <summary>
    /// Cria uma nova categoria
    /// </summary>
    /// <param name="dto">Dados da categoria</param>
    /// <returns>Categoria criada</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponseDto>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var category = await _categoryService.CreateAsync(dto);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<CategoryResponseDto>(
            data: category,
            message: "Categoria criada com sucesso",
            correlationId: correlationId
        );

        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = category.Id },
            value: response
        );
    }

    /// <summary>
    /// Atualiza uma categoria existente
    /// </summary>
    /// <param name="id">ID da categoria</param>
    /// <param name="dto">Dados para atualização</param>
    /// <returns>Categoria atualizada</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<CategoryResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCategoryDto dto)
    {
        var category = await _categoryService.UpdateAsync(id, dto);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<CategoryResponseDto>(
            data: category,
            message: "Categoria atualizada com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    /// <summary>
    /// Deleta (soft delete) uma categoria
    /// </summary>
    /// <param name="id">ID da categoria</param>
    /// <returns>Confirmação de exclusão</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _categoryService.DeleteAsync(id);
        var correlationId = GetCorrelationId();

        var response = new ApiResponse<object?>(
            data: null,
            message: "Categoria deletada com sucesso",
            correlationId: correlationId
        );

        return Ok(response);
    }

    private string? GetCorrelationId()
    {
        return HttpContext.Items[CorrelationIdHeader]?.ToString();
    }
}
