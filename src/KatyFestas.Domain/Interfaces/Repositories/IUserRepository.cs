using KatyFestas.Domain.Entities;

namespace KatyFestas.Domain.Interfaces.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
    Task<bool> EmailExistsAsync(string email);
    Task<IEnumerable<User>> GetByStoreAsync(Guid storeId);
}