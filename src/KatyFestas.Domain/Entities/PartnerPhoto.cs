using KatyFestas.Domain.Exceptions;

namespace KatyFestas.Domain.Entities;

public class PartnerPhoto : Entity
{
    // Propriedades
    public Guid PartnerId { get; private set; }
    public string Url { get; private set; } = string.Empty;
    public int Order { get; private set; }

    // Navegação
    public Partner Partner { get; private set; } = null!;

    protected PartnerPhoto() { }

    public static PartnerPhoto Create(Guid partnerId, string url, int order)
    {
        if (partnerId == Guid.Empty)
            throw new DomainException("Id do parceiro é obrigatório.");
        if (string.IsNullOrWhiteSpace(url))            
            throw new DomainException("Url é obrigatória.");
        if (order < 0)
            throw new DomainException("Ordem não pode ser negativa.");
        return new PartnerPhoto
        {
            PartnerId = partnerId,
            Url = url,
            Order = order
        };
    }
}