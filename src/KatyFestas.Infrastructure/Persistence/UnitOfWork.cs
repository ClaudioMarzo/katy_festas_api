using KatyFestas.Domain.Interfaces;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;
using KatyFestas.Infrastructure.Repositories;

namespace KatyFestas.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    private IStoreRepository? _stores;
    private IItemRepository? _items;
    private ICategoryRepository? _categories;
    private ICustomerRepository? _customers;
    private IUserRepository? _users;
    private IRentalRepository? _rentals;
    private IEstimateRepository? _estimates;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IStoreRepository Stores =>
        _stores ??= new StoreRepository(_context);

    public IItemRepository Items => 
        _items ??= new ItemRepository(_context);

    public ICategoryRepository Categories => 
        _categories ??= new CategoryRepository(_context);

    public ICustomerRepository Customers => 
        _customers ??= new CustomerRepository(_context);

    public IUserRepository Users => 
        _users ??= new UserRepository(_context);

    public IRentalRepository Rentals => 
        _rentals ??= new RentalRepository(_context);

    public IEstimateRepository Estimates => 
        _estimates ??= new EstimateRepository(_context);

    // Pendente - Partner será implementado futuramente
    public IPartnerRepository Partners => 
        throw new NotImplementedException("PartnerRepository será implementado em breve");

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
