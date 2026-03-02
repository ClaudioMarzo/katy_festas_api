using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class RentalItem : BaseEntity
{
    // Propriedades
    public int Quantity { get; private set; }

    // Propriedades e Relacionamento
    public Guid RentalId { get; private set; }
    public Guid ItemId { get; private set; }
    
    // Navegação
    public Rental Rental { get; private set; } = null!;
    public Item Item { get; private set; } = null!;

    protected RentalItem() { }

    public static RentalItem Create(Guid rentalId, Guid itemId, int quantity)
    {
        if (rentalId == Guid.Empty)
            throw new DomainException("Id do aluguel é obrigatório.");
        if (itemId == Guid.Empty)
            throw new DomainException("Id do item é obrigatório.");
        if (quantity <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");
            
        return new RentalItem
        {
            RentalId = rentalId,
            ItemId = itemId,
            Quantity = quantity
        };
    }
}