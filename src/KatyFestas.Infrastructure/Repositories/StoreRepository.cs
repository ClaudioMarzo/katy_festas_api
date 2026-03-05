using KatyFestas.Domain.Entities;
using KatyFestas.Domain.Interfaces.Repositories;
using KatyFestas.Infrastructure.Persistence;

namespace KatyFestas.Infrastructure.Repositories;

public class StoreRepository : BaseRepository<Store>, IStoreRepository
{
    public StoreRepository(AppDbContext context) : base(context)
    {
    }
}
