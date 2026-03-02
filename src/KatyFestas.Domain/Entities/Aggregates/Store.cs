using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class Store : BaseEntity
{
    private readonly List<Category> _categories = [];
    private readonly List<Item> _items = [];
    private readonly List<User> _users = [];
    private readonly List<Estimate> _estimates = [];
    private readonly List<Rental> _rentals = [];

    // Propriedades
    public int StoreId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string WhatsappPhone { get; private set; } = string.Empty;

    // Navegação
    public IReadOnlyCollection<Category> Categories => _categories.AsReadOnly();
    public IReadOnlyCollection<Item> Items => _items.AsReadOnly();
    public IReadOnlyCollection<User> Users => _users.AsReadOnly();
    public IReadOnlyCollection<Estimate> Estimates => _estimates.AsReadOnly();
    public IReadOnlyCollection<Rental> Rentals => _rentals.AsReadOnly();


    protected Store() { }

    public static Store Create(string name, string whatsappPhone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(whatsappPhone))
            throw new DomainException("Telefone do WhatsApp é obrigatório.");
            
        return new Store
        {
            Name = name,
            WhatsappPhone = whatsappPhone
        };
    }

    public void Update(string? name = null, string? whatsappPhone = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Nome é obrigatório.");
            Name = name;
        }
        if (whatsappPhone != null)        {
            if (string.IsNullOrWhiteSpace(whatsappPhone))
                throw new DomainException("Telefone do WhatsApp é obrigatório.");
            WhatsappPhone = whatsappPhone;
        }
        SetUpdatedAt();
    }
}