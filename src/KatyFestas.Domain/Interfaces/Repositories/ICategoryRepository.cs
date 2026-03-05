using KatyFestas.Domain.Entities;

namespace KatyFestas.Domain.Interfaces.Repositories;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IEnumerable<Category>> GetByStoreAsync(Guid storeId);
    Task<bool> ExistsAsync(Guid id);
    Task<bool> ExistsByNameInStoreAsync(Guid storeId, string name);
}