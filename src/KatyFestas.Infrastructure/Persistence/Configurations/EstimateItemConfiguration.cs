using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class EstimateItemConfiguration : IEntityTypeConfiguration<EstimateItem>
{
    public void Configure(EntityTypeBuilder<EstimateItem> builder)
    {
        builder.ToTable("estimate_items");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
        
        builder.Property(x => x.Quantity)
            .HasColumnName("quantity")
            .IsRequired();
        
        builder.Property(x => x.EstimateId)
            .HasColumnName("estimate_id");
        
        builder.Property(x => x.ItemId)
            .HasColumnName("item_id");
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        // Índices
        builder.HasIndex(x => new { x.EstimateId, x.ItemId })
            .IsUnique()
            .HasDatabaseName("ix_estimate_items_estimate_id_item_id");
        
        // Relacionamentos
        builder.HasOne(x => x.Estimate)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.EstimateId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Item)
            .WithMany()
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
