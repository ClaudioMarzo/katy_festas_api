using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class ItemConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("items");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever(); // Guid gerado na entidade
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(150);
        
        builder.Property(x => x.Description)
            .HasColumnName("description")
            .IsRequired()
            .HasMaxLength(1000);
        
        builder.Property(x => x.Price)
            .HasColumnName("price")
            .IsRequired()
            .HasColumnType("decimal(18,2)");
        
        builder.Property(x => x.StockQuantity)
            .HasColumnName("stock_quantity")
            .IsRequired();
        
        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);
        
        builder.Property(x => x.StoreId)
            .HasColumnName("store_id");
        
        builder.Property(x => x.CategoryId)
            .HasColumnName("category_id");
        
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
            .HasDatabaseName("ix_items_store_id");
        
        builder.HasIndex(x => x.CategoryId)
            .HasDatabaseName("ix_items_category_id");
        
        builder.HasIndex(x => x.IsActive)
            .HasDatabaseName("ix_items_is_active");
        
        builder.HasIndex(x => new { x.StoreId, x.Name })
            .HasDatabaseName("ix_items_store_id_name");
        
        // Relacionamentos
        builder.HasOne(x => x.Store)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(x => x.Category)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        // Acesso à coleção privada _photos
        builder.HasMany(x => x.Photos)
            .WithOne(x => x.Item)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Configuração da navegação privada
        builder.Navigation(x => x.Photos)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
