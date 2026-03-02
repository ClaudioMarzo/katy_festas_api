using KatyFestas.Domain.Interfaces.Repositories;

namespace KatyFestas.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IItemRepository Items { get; }
    IRentalRepository Rentals { get; }
    IEstimateRepository Estimates { get; }
    IPartnerRepository Partners { get; }
    ICategoryRepository Categories { get; }
    ICustomerRepository Customers { get; }
    IUserRepository Users { get; }

    /// <summary>
    /// Persiste todas as operações pendentes em uma única transação.
    /// Ou tudo é salvo ou nada é salvo.
    /// </summary>
    Task<int> CommitAsync();
}