using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class EstimateItem : Entity
{
    // Propriedades
    public int Quantity { get; private set; }
    
    // Relacionamento
    public Guid EstimateId { get; private set; }
    public Guid ItemId { get; private set; }

    // Navegação
    public Estimate Estimate { get; private set; } = null!;
    public Item Item { get; private set; } = null!;

    protected EstimateItem() { }

    public static EstimateItem Create(Guid estimateId, Guid itemId, int quantity)
    {
        if (estimateId == Guid.Empty)
            throw new DomainException("Id do orçamento é obrigatório.");
        if (itemId == Guid.Empty)
            throw new DomainException("Id do item é obrigatório.");
        if (quantity <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");
        
        return new EstimateItem
        {
            EstimateId = estimateId,
            ItemId = itemId,
            Quantity = quantity
        };
    }
}