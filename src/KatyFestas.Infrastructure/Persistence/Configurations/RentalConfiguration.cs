using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("rentals");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
        
        builder.Property(x => x.Status)
            .HasColumnName("status")
            .IsRequired()
            .HasConversion(
                v => v.ToString().ToUpperInvariant(),
                v => Enum.Parse<RentalStatus>(v, ignoreCase: true))
            .HasMaxLength(50);
        
        builder.Property(x => x.EventDate)
            .HasColumnName("event_date")
            .IsRequired();
        
        builder.Property(x => x.Notes)
            .HasColumnName("notes")
            .HasMaxLength(1000);
        
        builder.Property(x => x.StoreId)
            .HasColumnName("store_id");
        
        builder.Property(x => x.CustomerId)
            .HasColumnName("customer_id");
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");
        
        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at");
        
        // Soft Delete Filter
        builder.HasQueryFilter(x => x.DeletedAt == null);
        
        // Índices
        builder.HasIndex(x => x.StoreId)
            .HasDatabaseName("ix_rentals_store_id");
        
        builder.HasIndex(x => x.CustomerId)
            .HasDatabaseName("ix_rentals_customer_id");
        
        builder.HasIndex(x => x.Status)
            .HasDatabaseName("ix_rentals_status");
        
        builder.HasIndex(x => x.EventDate)
            .HasDatabaseName("ix_rentals_event_date");
        
        // Relacionamentos
        builder.HasOne(x => x.Store)
            .WithMany(x => x.Rentals)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Customer)
            .WithMany(x => x.Rentals)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Rental)
            .HasForeignKey(x => x.RentalId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
