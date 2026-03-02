using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Exceptions;
using KatyFestas.Domain.Interfaces;
using KatyFestas.Application.DTOs.Category;
using KatyFestas.Application.Interfaces.Services;

namespace KatyFestas.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<CategoryResponseDto> GetByIdAsync(Guid id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id)
            ?? throw new NotFoundException($"Categoria com ID {id} não encontrada");

        return MapToResponseDto(category);
    }

    public async Task<IEnumerable<CategoryResponseDto>> GetByStoreAsync(Guid storeId)
    {
        var categories = await _unitOfWork.Categories.GetByStoreAsync(storeId);
        return categories.Select(MapToResponseDto);
    }

    public async Task<CategoryResponseDto> CreateAsync(CreateCategoryDto dto)
    {
        var category = Category.Create(dto.StoreId, dto.Name);

        await _unitOfWork.Categories.AddAsync(category);
        await _unitOfWork.CommitAsync();

        // Recarrega com relacionamentos
        var createdCategory = await _unitOfWork.Categories.GetByIdAsync(category.Id);
        return MapToResponseDto(createdCategory!);
    }

    public async Task<CategoryResponseDto> UpdateAsync(Guid id, UpdateCategoryDto dto)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id)
            ?? throw new NotFoundException($"Categoria com ID {id} não encontrada");

        // Atualiza apenas campos que foram enviados
        if (!string.IsNullOrWhiteSpace(dto.Name))
        {
            category.Update(dto.Name);
        }

        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.CommitAsync();

        // Recarrega com relacionamentos
        var updatedCategory = await _unitOfWork.Categories.GetByIdAsync(id);
        return MapToResponseDto(updatedCategory!);
    }

    public async Task DeleteAsync(Guid id)
    {
        var category = await _unitOfWork.Categories.GetByIdAsync(id)
            ?? throw new NotFoundException($"Categoria com ID {id} não encontrada");

        category.Delete();
        await _unitOfWork.Categories.UpdateAsync(category);
        await _unitOfWork.CommitAsync();
    }

    private static CategoryResponseDto MapToResponseDto(Category category)
    {
        return new CategoryResponseDto
        {
            Id = category.Id,
            Name = category.Name,
            StoreId = category.StoreId,
            StoreName = category.Store.Name,
            CreatedAt = category.CreatedAt,
            UpdatedAt = category.UpdatedAt
        };
    }
}
