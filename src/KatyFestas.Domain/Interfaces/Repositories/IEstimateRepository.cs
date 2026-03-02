using KatyFestas.Domain.Entities;

namespace KatyFestas.Domain.Interfaces.Repositories;

public interface IEstimateRepository : IBaseRepository<Estimate>
{
    Task<IEnumerable<Estimate>> GetByStoreAsync(Guid storeId, int page, int pageSize);
    Task<int> CountByStoreAsync(Guid storeId);
}