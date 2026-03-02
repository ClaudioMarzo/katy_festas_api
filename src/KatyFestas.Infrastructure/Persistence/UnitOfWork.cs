using KatyFestas.Domain.Interfaces;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;
using KatyFestas.Infrastructure.Repositories;

namespace KatyFestas.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IItemRepository? _items;
    private ICategoryRepository? _categories;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IItemRepository Items => 
        _items ??= new ItemRepository(_context);

    public ICategoryRepository Categories => 
        _categories ??= new CategoryRepository(_context);

    // Pendente - Lançam NotImplementedException até criar os repositórios
    public IRentalRepository Rentals => 
        throw new NotImplementedException("RentalRepository será implementado em breve");

    public IEstimateRepository Estimates => 
        throw new NotImplementedException("EstimateRepository será implementado em breve");

    public IPartnerRepository Partners => 
        throw new NotImplementedException("PartnerRepository será implementado em breve");

    public ICustomerRepository Customers => 
        throw new NotImplementedException("CustomerRepository será implementado em breve");

    public IUserRepository Users => 
        throw new NotImplementedException("UserRepository será implementado em breve");

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
