using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class Category : BaseEntity
{
    private readonly List<Item> _items = [];
    
    // Propriedades
    public string Name { get; private set; } = string.Empty;

    // Relacionamento
    public Guid StoreId { get; private set; }
    // Navegação
    public Store Store { get; private set; } = null!;
    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();

    protected Category() {}

    public static Category Create(Guid storeId, string name)
    {
        if(storeId == Guid.Empty)
            throw new DomainException("StoreId é obrigatório.");
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome é obrigatório.");

        return new Category
        {
            StoreId = storeId,
            Name = name
        };
    }

    public void Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome é obrigatório.");
        Name = name;
        SetUpdatedAt();
    }
    public void Delete()
    {
        SetDeletedAt();
    }
}