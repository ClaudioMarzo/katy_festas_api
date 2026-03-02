using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KatyFestas.Infrastructure.Repositories;

public class RentalRepository : BaseRepository<Rental>, IRentalRepository
{
    public RentalRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<Rental?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Customer)
            .Include(x => x.Items)
                .ThenInclude(x => x.Item)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public override async Task<IEnumerable<Rental>> GetAllAsync()
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Customer)
            .Include(x => x.Items)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<Rental>> GetByCustomerIdAsync(Guid customerId, int page, int pageSize)
    {
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Customer)
            .Include(x => x.Items)
                .ThenInclude(x => x.Item)
            .Where(x => x.CustomerId == customerId)
            .OrderByDescending(x => x.CreatedAt)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountByCustomerIdAsync(Guid customerId)
    {
        return await _dbSet
            .Where(x => x.CustomerId == customerId)
            .CountAsync();
    }

    public async Task<IEnumerable<Rental>> GetActiveRentalsByPartnerIdAsync(Guid partnerId, int page, int pageSize)
    {
        // Nota: A lógica de Partner ainda não está implementada
        // Por enquanto, retorna rentals ativos (não finalizados ou cancelados)
        return await _dbSet
            .Include(x => x.Store)
            .Include(x => x.Customer)
            .Include(x => x.Items)
            .Where(x => x.Status != RentalStatus.Completed 
                     && x.Status != RentalStatus.Cancelled 
                     && x.Status != RentalStatus.Deleted)
            .OrderByDescending(x => x.EventDate)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<int> CountActiveRentalsByPartnerIdAsync(Guid partnerId)
    {
        return await _dbSet
            .Where(x => x.Status != RentalStatus.Completed 
                     && x.Status != RentalStatus.Cancelled 
                     && x.Status != RentalStatus.Deleted)
            .CountAsync();
    }
}
