using KatyFestas.Domain.Entities;

namespace KatyFestas.Domain.Interfaces.Repositories;

public interface IPartnerRepository : IBaseRepository<Partner>
{
    Task<IEnumerable<Partner>> GetActivePartnersAsync(int page, int pageSize);
    Task<int> CountActivePartnersAsync();
}