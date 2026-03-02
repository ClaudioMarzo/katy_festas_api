using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class ItemPhoto : Entity
{
    // Propriedades
    public string Url { get; private set; } = string.Empty;
    public int Order { get; private set; }

    // Relacionamento
    public Guid ItemId { get; private set; }
    
    // Navegação
    public Item Item { get; private set; } = null!;

    protected ItemPhoto() { }

    public static ItemPhoto Create(Guid itemId, string url, int order)
    {
        if (itemId == Guid.Empty)
            throw new DomainException("Id do item é obrigatório.");
        if (string.IsNullOrWhiteSpace(url))
            throw new DomainException("Url é obrigatória.");
        if (order < 0)
            throw new DomainException("Ordem não pode ser negativa.");

        return new ItemPhoto
        {
            ItemId = itemId,
            Url = url,
            Order = order
        };
    }
}