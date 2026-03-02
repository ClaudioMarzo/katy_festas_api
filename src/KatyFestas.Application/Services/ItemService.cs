using KatyFestas.Application.Common;
using KatyFestas.Application.DTOs.Item;
using KatyFestas.Application.Interfaces.Services;
using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Exceptions;
using KatyFestas.Domain.Interfaces;

namespace KatyFestas.Application.Services;

public class ItemService : IItemService
{
    private readonly IUnitOfWork _unitOfWork;

    public ItemService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ItemResponseDto> GetByIdAsync(Guid id)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(id)
            ?? throw new NotFoundException($"Item com ID {id} não encontrado");

        return MapToResponseDto(item);
    }

    public async Task<PagedResponse<ItemResponseDto>> GetByStoreAsync(Guid storeId, PagedRequest request)
    {
        var items = await _unitOfWork.Items.GetByStoreAsync(storeId, request.Page, request.PageSize);
        var totalItems = await _unitOfWork.Items.CountByStoreAsync(storeId);
        var totalPages = (int)Math.Ceiling(totalItems / (double)request.PageSize);

        return new PagedResponse<ItemResponseDto>
        {
            Data = items.Select(MapToResponseDto),
            Page = request.Page,
            PageSize = request.PageSize,
            TotalItems = totalItems,
            TotalPages = totalPages
        };
    }

    public async Task<ItemResponseDto> CreateAsync(CreateItemDto dto)
    {
        // Valida se categoria existe
        var categoryExists = await _unitOfWork.Categories.ExistsAsync(dto.CategoryId);
        if (!categoryExists)
            throw new NotFoundException($"Categoria com ID {dto.CategoryId} não encontrada");

        // Cria item
        var item = Item.Create(
            dto.StoreId,
            dto.CategoryId,
            dto.Name,
            dto.Description,
            dto.Price,
            dto.StockQuantity
        );

        await _unitOfWork.Items.AddAsync(item);
        await _unitOfWork.CommitAsync();

        // Recarrega com relacionamentos
        var createdItem = await _unitOfWork.Items.GetByIdAsync(item.Id);
        return MapToResponseDto(createdItem!);
    }

    public async Task<ItemResponseDto> UpdateAsync(Guid id, UpdateItemDto dto)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(id)
            ?? throw new NotFoundException($"Item com ID {id} não encontrado");

        // Atualiza apenas campos que foram enviados
        item.Update(
            name: dto.Name,
            description: dto.Description,
            price: dto.Price,
            stockQuantity: dto.StockQuantity
        );

        await _unitOfWork.Items.UpdateAsync(item);
        await _unitOfWork.CommitAsync();

        // Recarrega com relacionamentos
        var updatedItem = await _unitOfWork.Items.GetByIdAsync(id);
        return MapToResponseDto(updatedItem!);
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(id)
            ?? throw new NotFoundException($"Item com ID {id} não encontrado");

        item.Delete();
        await _unitOfWork.Items.UpdateAsync(item);
        await _unitOfWork.CommitAsync();
    }

    public async Task ActivateAsync(Guid id)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(id)
            ?? throw new NotFoundException($"Item com ID {id} não encontrado");

        item.Activate();
        await _unitOfWork.Items.UpdateAsync(item);
        await _unitOfWork.CommitAsync();
    }

    public async Task DeactivateAsync(Guid id)
    {
        var item = await _unitOfWork.Items.GetByIdAsync(id)
            ?? throw new NotFoundException($"Item com ID {id} não encontrado");

        item.Deactivate();
        await _unitOfWork.Items.UpdateAsync(item);
        await _unitOfWork.CommitAsync();
    }

    private static ItemResponseDto MapToResponseDto(Item item)
    {
        return new ItemResponseDto
        {
            Id = item.Id,
            Name = item.Name,
            Description = item.Description,
            Price = item.Price,
            StockQuantity = item.StockQuantity,
            IsActive = item.IsActive,
            StoreId = item.StoreId,
            StoreName = item.Store.Name,
            CategoryId = item.CategoryId,
            CategoryName = item.Category.Name,
            Photos = item.Photos.Select(p => new ItemPhotoDto
            {
                Id = p.Id,
                Url = p.Url,
                Order = p.Order
            }).ToList(),
            CreatedAt = item.CreatedAt,
            UpdatedAt = item.UpdatedAt
        };
    }
}
