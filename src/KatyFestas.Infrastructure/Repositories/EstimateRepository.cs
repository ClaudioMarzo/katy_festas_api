using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KatyFestas.Infrastructure.Repositories;

public class EstimateRepository : IEstimateRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Estimate> _dbSet;

    public EstimateRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<Estimate>();
    }

    public async Task<Estimate?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Items)
                .ThenInclude(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<IEnumerable<Estimate>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Items)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Estimate entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task UpdateAsync(Estimate entity)
    {
        _dbSet.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _dbSet.Remove(entity); // Hard delete para Entity
        }
    }

    public async Task<IEnumerable<Estimate>> GetByStoreAsync(Guid storeId, int page, int pageSize)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Items)
                .ThenInclude(x => x.Item)
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
