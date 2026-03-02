using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KatyFestas.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // ═══════════════════════════════════════════════════════════════
    // AGGREGATE ROOTS - Entidades principais acessadas diretamente
    // ═══════════════════════════════════════════════════════════════
    
    public DbSet<Store> Stores => Set<Store>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Item> Items => Set<Item>();
    
    // PENDENTES 
    // public DbSet<Customer> Customers => Set<Customer>();
    // public DbSet<User> Users => Set<User>();
    // public DbSet<Partner> Partners => Set<Partner>();
    // public DbSet<Rental> Rentals => Set<Rental>();
    // public DbSet<Estimate> Estimates => Set<Estimate>();

    // ═══════════════════════════════════════════════════════════════
    // ENTIDADES FILHAS - Acessadas via Include() do Aggregate Root
    // ═══════════════════════════════════════════════════════════════
    // ItemPhoto       -> Acessado via Item.Photos
    // RentalItem      -> Acessado via Rental.Items
    // PartnerPhoto    -> Acessado via Partner.Photos
    // EstimateItem    -> Acessado via Estimate.Items

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    /// <summary>
    /// Sobrescreve SaveChangesAsync para adicionar comportamentos automáticos:
    /// - UpdatedAt preenchido automaticamente em Modified
    /// - DeletedAt preenchido automaticamente em Deleted (soft delete)
    /// </summary>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<BaseEntity>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    // CreatedAt já vem preenchido do construtor da BaseEntity
                    break;
                
                case EntityState.Modified:
                    // Preenche UpdatedAt automaticamente
                    entry.Entity.SetUpdatedAt();
                    break;
                
                case EntityState.Deleted:  
                    // Converte hard delete em soft delete
                    entry.State = EntityState.Modified;
                    entry.Entity.SetDeletedAt();
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
}
