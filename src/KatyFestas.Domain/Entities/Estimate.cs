using KatyFestas.Domain.EvaluateAttribute;
using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class Estimate : Entity
{
    private readonly List<EstimateItem> _items = [];
    
    // Propriedades
    public Guid StoreId { get; private set; }
    public string CustomerName { get; private set; } = string.Empty;
    public string? CustomerEmail { get; private set; }
    public string CustomerPhone { get; private set; } = string.Empty;
    public string EventDescription { get; private set; } = string.Empty;
    public DateTime EventForecastDate { get; private set; }

    // Navegação
    public Store Store { get; private set; } = null!;
    public IReadOnlyCollection<EstimateItem> Items => _items.AsReadOnly();

    protected Estimate() { }

    public static Estimate Create(
        Guid storeId,
        string customerName,
        string? customerEmail,
        string customerPhone,
        string eventDescription,
        DateTime eventForecastDate)
    {
        if (storeId == Guid.Empty)
            throw new DomainException("StoreId é obrigatório.");
        if (string.IsNullOrWhiteSpace(customerName))
            throw new DomainException("CustomerName é obrigatório.");
        if (!string.IsNullOrWhiteSpace(customerEmail) && !EmailEvaluateAttribute.IsValid(customerEmail))
            throw new DomainException("Email está inválido.");
        if (string.IsNullOrWhiteSpace(customerPhone))
            throw new DomainException("CustomerPhone é obrigatório.");
        if (string.IsNullOrWhiteSpace(eventDescription))
            throw new DomainException("EventDescription é obrigatório.");
        return new Estimate
        {
            StoreId = storeId,
            CustomerName = customerName,
            CustomerEmail = string.IsNullOrWhiteSpace(customerEmail) ? null : customerEmail.ToLowerInvariant().Trim(),
            CustomerPhone = customerPhone,
            EventDescription = eventDescription,
            EventForecastDate = eventForecastDate
        };
    }

    public void AddItem(Guid itemId, int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Quantidade deve ser maior que zero.");

        _items.Add(EstimateItem.Create(Id, itemId, quantity));
    }
}