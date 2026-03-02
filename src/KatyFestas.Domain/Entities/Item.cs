using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class Item : BaseEntity
{
    private readonly List<ItemPhoto> _photos = [];
    
    // Propriedades
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int StockQuantity { get; private set; }
    public bool IsActive { get; private set; } = true;

    // Relacionamento
    public Guid StoreId { get; private set; }
    public Guid CategoryId { get; private set; }
    
    // Navegação
    public Store Store { get; private set; } = null!;
    public Category Category { get; private set; } = null!;
    public IReadOnlyCollection<ItemPhoto> Photos => _photos.AsReadOnly();

    protected Item() { }

    public static Item Create(Guid storeId, Guid categoryId, string name, string description, decimal price, int stockQuantity)
    {
        if (storeId == Guid.Empty)
            throw new DomainException("StoreId é obrigatório.");
        if (categoryId == Guid.Empty)
            throw new DomainException("CategoryId é obrigatório.");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Descrição é obrigatória.");
        if (price <= 0)
            throw new DomainException("Preço deve ser maior que zero.");
        if (stockQuantity < 0)
            throw new DomainException("Quantidade em estoque não pode ser negativa.");

        return new Item
        {
            StoreId = storeId,
            CategoryId = categoryId,
            Name = name,
            Description = description,
            Price = price,
            StockQuantity = stockQuantity,
        };
    }

    public void Update(string? name = null, string? description = null, decimal? price = null, int? stockQuantity = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Nome é obrigatório.");
            Name = name;
        }

        if (description != null)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new DomainException("Descrição é obrigatória.");
            Description = description;
        }

        if (price != null)
        {
            if (price <= 0)
                throw new DomainException("Preço deve ser maior que zero.");
            Price = price.Value;
        }

        if (stockQuantity != null)
        {
            if (stockQuantity < 0)
                throw new DomainException("Quantidade em estoque não pode ser negativa.");
            StockQuantity = stockQuantity.Value;
        }
        SetUpdatedAt();
    }
    public void DecrementStock(int quantity)
    {
        if (quantity > StockQuantity)
            throw new DomainException($"Estoque insuficiente. Disponível: {StockQuantity}, solicitado: {quantity}");

        StockQuantity -= quantity;
        SetUpdatedAt();
    }

    public void IncrementStock(int quantity)
    {
        StockQuantity += quantity;
        SetUpdatedAt();
    }
    public void AddPhoto(string url, int order)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new DomainException("URL da foto é obrigatória.");
        if (order < 0)
            throw new DomainException("Ordem não pode ser negativa.");
        
        if (_photos.Any(p => p.Order == order))
            throw new DomainException($"Já existe uma foto com a ordem {order}.");
        
        var photo = ItemPhoto.Create(Id, url, order);
        _photos.Add(photo);
        SetUpdatedAt();
    }
    public void RemovePhoto(Guid photoId)
    {
        var photo = _photos.FirstOrDefault(p => p.Id == photoId);
        if (photo == null)
            throw new DomainException("Foto não encontrada.");
        
        _photos.Remove(photo);
        SetUpdatedAt();
    }
    public void Deactivate()
    {
        if (DeletedAt.HasValue)
            throw new DomainException("Cliente deletado não pode ser desativado.");
        
        IsActive = false;
        SetUpdatedAt();
    }
    public void Activate()
    {
        if (DeletedAt.HasValue)
            throw new DomainException("Cliente deletado não pode ser ativado.");
        
        IsActive = true;
        SetUpdatedAt();
    }

    public void Delete()
    {
        IsActive = false;
        SetDeletedAt();
    }
}