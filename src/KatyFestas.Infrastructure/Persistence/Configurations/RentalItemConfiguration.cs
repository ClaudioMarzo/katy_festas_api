using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class RentalItemConfiguration : IEntityTypeConfiguration<RentalItem>
{
    public void Configure(EntityTypeBuilder<RentalItem> builder)
    {
        builder.ToTable("rental_items");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
        
        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();
        
        builder.Property(x => x.RentalId)
            .HasColumnName("rental_id");
        
        builder.Property(x => x.ItemId)
            .HasColumnName("item_id");
        
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
        builder.HasIndex(x => new { x.RentalId, x.ItemId })
            .IsUnique()
            .HasDatabaseName("ix_rental_items_rental_id_item_id");
        
        // Relacionamentos
        builder.HasOne(x => x.Rental)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.RentalId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
