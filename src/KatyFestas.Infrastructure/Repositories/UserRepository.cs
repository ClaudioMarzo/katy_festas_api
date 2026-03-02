using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KatyFestas.Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbSet
            .Include(x => x.Store)
            .FirstOrDefaultAsync(x => x.Email == email.ToLowerInvariant().Trim());
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _dbSet
            .AnyAsync(x => x.Email == email.ToLowerInvariant().Trim());
    }

    public async Task<IEnumerable<User>> GetByStoreAsync(Guid storeId)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Where(x => x.StoreId == storeId && x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public override async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Store)
            .Where(x => x.IsActive)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public override async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Store)
            .FirstOrDefaultAsync(x => x.Id == id);
    }
}
