using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KatyFestas.Infrastructure.Repositories;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<IEnumerable<Category>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Store)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<IEnumerable<Category>> GetByStoreAsync(Guid storeId)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Where(x => x.StoreId == storeId)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }
}
