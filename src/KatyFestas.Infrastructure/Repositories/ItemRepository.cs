using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KatyFestas.Infrastructure.Repositories;

public class ItemRepository : BaseRepository<Item>, IItemRepository
{
    public ItemRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Item?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Category)
            .Include(x => x.Photos.OrderBy(p => p.Order))
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Category)
            .Include(x => x.Photos.OrderBy(p => p.Order))
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Item>> GetByStoreAsync(Guid storeId, int page, int pageSize)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Category)
            .Include(x => x.Photos.OrderBy(p => p.Order))
            .Where(x => x.StoreId == storeId)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByStoreAsync(Guid storeId)
    {
        return await _dbSet
            .Where(x => x.StoreId == storeId)
            .CountAsync();
    }
}
