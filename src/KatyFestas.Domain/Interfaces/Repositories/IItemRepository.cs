
using KatyFestas.Domain.Entities;

namespace KatyFestas.Domain.Interfaces.Repositories;
public interface IItemRepository : IBaseRepository<Item>
{
    Task<IEnumerable<Item>> GetByStoreAsync(Guid storeId, int page, int pageSize);
    Task<int> CountByStoreAsync(Guid storeId);
}