using KatyFestas.Domain.Exceptions;
using KatyFestas.Domain.EvaluateAttribute;

namespace KatyFestas.Domain.Entities;

public class Customer : BaseEntity
{
    private readonly List<Rental> _rentals = [];
    
    // Propriedades
    public string Name { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public string Phone { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = true;

    // Navegação
    public IReadOnlyCollection<Rental> Rentals => _rentals.AsReadOnly();
   
    protected Customer() {}

    public static Customer Create(string name, string phone, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome é obrigatório.");
        if (!string.IsNullOrWhiteSpace(email) && !EmailEvaluateAttribute.IsValid(email))
            throw new DomainException("Email está inválido.");
        if (string.IsNullOrWhiteSpace(phone))
            throw new DomainException("Telefone é obrigatório.");
        
        return new Customer
        {
            Name = name,
            Email = string.IsNullOrWhiteSpace(email) ? null : email.ToLowerInvariant().Trim(),
            Phone = phone,
        };
    }
    public void Update(string? name = null, string? email = null, string? phone = null)
    {
        if (name != null)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException("Nome é obrigatório.");
            Name = name;
        }

        if (email != null)
        {
            if (!string.IsNullOrWhiteSpace(email) && !EmailEvaluateAttribute.IsValid(email))
                throw new DomainException("Email está inválido.");
            Email = email.ToLowerInvariant().Trim();
        }

        if (phone != null)
        {
            if (string.IsNullOrWhiteSpace(phone))
                throw new DomainException("Telefone é obrigatório.");
            Phone = phone;
        }

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