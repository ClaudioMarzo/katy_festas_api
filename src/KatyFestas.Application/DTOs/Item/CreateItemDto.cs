namespace KatyFestas.Application.DTOs.Item;

public record CreateItemDto(
    Guid StoreId,
    Guid CategoryId,
    string Name,
    string Description,
    decimal Price,
    int StockQuantity
);
