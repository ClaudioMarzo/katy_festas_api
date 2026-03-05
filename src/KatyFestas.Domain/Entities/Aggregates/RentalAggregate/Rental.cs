using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class Rental : BaseEntity
{
    private readonly List<RentalItem> _items = [];

    // Propriedades
    public RentalStatus Status { get; private set; }
    public DateTime EventDate { get; private set; }
    public string? Notes { get; private set; }
    
    // Relacionamento
    public Guid StoreId { get; private set; }
    public Guid CustomerId { get; private set; }

    // Navegação
    public Store Store { get; private set; } = null!;
    public Customer Customer { get; private set; } = null!;
    public IReadOnlyCollection<RentalItem> Items => _items.AsReadOnly();

    protected Rental() { }

    public static Rental Create(Guid storeId, Guid customerId, DateTime eventDate, string? notes)
    {
        if (storeId == Guid.Empty)
            throw new DomainException("Id da loja é obrigatório.");
        if (customerId == Guid.Empty)
            throw new DomainException("Id do cliente é obrigatório.");
        if (eventDate <= DateTime.UtcNow)
            throw new DomainException("Data do evento deve ser no futuro.");
        return new Rental
        {
            StoreId = storeId,
            CustomerId = customerId,
            EventDate = eventDate,
            Notes = notes,
            Status = RentalStatus.Requested
        };
    }

    public void AddItem(Guid itemId, int quantity)
    {
        if (Status != RentalStatus.Requested)
            throw new DomainException("Itens só podem ser adicionados em aluguéis com status Requested.");

        if (quantity <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");

        _items.Add(RentalItem.Create(Id, itemId, quantity));
        SetUpdatedAt();
    }

    // Máquina de estados — cada transição tem sua regra
    public void StartReview()
    {
        if (Status != RentalStatus.Requested)
            throw new DomainException("Apenas aluguéis solicitados podem entrar em revisão.");

        Status = RentalStatus.InReview;
        SetUpdatedAt();
    }

    public void Confirm()
    {
        if (Status != RentalStatus.InReview)
            throw new DomainException("Apenas aluguéis em revisão podem ser confirmados.");

        Status = RentalStatus.Confirmed;
        SetUpdatedAt();
    }

    public void Complete()
    {
        if (Status != RentalStatus.Confirmed)
            throw new DomainException("Apenas aluguéis confirmados podem ser finalizados.");

        Status = RentalStatus.Completed;
        SetUpdatedAt();
    }

    public void Cancel()
    {
        if (Status == RentalStatus.Completed)
            throw new DomainException("Aluguéis finalizados não podem ser cancelados.");

        Status = RentalStatus.Cancelled;
        SetUpdatedAt();
    }

    public void Delete()
    {
        Status = RentalStatus.Deleted;
        SetDeletedAt();
    }
}