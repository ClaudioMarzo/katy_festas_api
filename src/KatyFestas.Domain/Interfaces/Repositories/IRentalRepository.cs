using KatyFestas.Domain.Entities;

namespace KatyFestas.Domain.Interfaces.Repositories;

public interface IRentalRepository : IBaseRepository<Rental>
{
    Task<IEnumerable<Rental>> GetByCustomerIdAsync(Guid customerId, int page, int pageSize);
    Task<int> CountByCustomerIdAsync(Guid customerId);
    Task<IEnumerable<Rental>> GetActiveRentalsByPartnerIdAsync(Guid partnerId, int page, int pageSize);
    Task<int> CountActiveRentalsByPartnerIdAsync(Guid partnerId);
}