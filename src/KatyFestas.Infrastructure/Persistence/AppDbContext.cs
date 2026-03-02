using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KatyFestas.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Rental> Rentals => Set<Rental>();
    public DbSet<Partner> Partners => Set<Partner>();
    public DbSet<Estimate> Estimates => Set<Estimate>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<ItemPhoto> ItemPhotos => Set<ItemPhoto>();
    public DbSet<RentalItem> RentalItems => Set<RentalItem>();
    public DbSet<PartnerPhoto> PartnerPhotos => Set<PartnerPhoto>();
    public DbSet<EstimateItem> EstimateItems => Set<EstimateItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
