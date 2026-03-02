using KatyFestas.Domain.EvaluateAttribute;
using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class Partner : BaseEntity
{
    private readonly List<PartnerPhoto> _photos = [];

    // Propriedades
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Contact { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public bool IsActive { get; private set; } = true;

    // Navegação
    public IReadOnlyCollection<PartnerPhoto> Photos => _photos.AsReadOnly();

    protected Partner() { }

    public static Partner Create(string name, string description, string contact, string? email = null)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Nome é obrigatório.");
        if (string.IsNullOrWhiteSpace(description))
            throw new DomainException("Descrição é obrigatória.");  
        if (string.IsNullOrWhiteSpace(contact))
            throw new DomainException("Contato é obrigatório.");
        if (!string.IsNullOrWhiteSpace(email) && !EmailEvaluateAttribute.IsValid(email))
            throw new DomainException("Email está inválido.");
            
        return new Partner
        {
            Name = name,
            Description = description,
            Contact = contact,
            Email = string.IsNullOrWhiteSpace(email) ? null : email.ToLowerInvariant().Trim(),
        };
    }

    public void Update(string? name = null, string? description = null, string? contact = null, string? email = null)
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
        if (contact != null)        {
            if (string.IsNullOrWhiteSpace(contact))
                throw new DomainException("Contato é obrigatório.");
            Contact = contact;
        }
        if (email != null)
        {
            if (!string.IsNullOrWhiteSpace(email) && !EmailEvaluateAttribute.IsValid(email))
                throw new DomainException("Email está inválido.");
            Email = string.IsNullOrWhiteSpace(email) ? null : email.ToLowerInvariant().Trim();
        }
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
        
        var photo = PartnerPhoto.Create(Id, url, order);
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
            throw new DomainException("Parceiro deletado não pode ser desativado.");
        
        IsActive = false;
        SetUpdatedAt();
    }
    public void Activate()
    {
        if (DeletedAt.HasValue)
            throw new DomainException("Parceiro deletado não pode ser ativado.");
        
        IsActive = true;
        SetUpdatedAt();
    }

    public void Delete()
    {
        IsActive = false;
        SetDeletedAt();
    }
}