using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class ItemPhotoConfiguration : IEntityTypeConfiguration<ItemPhoto>
{
    public void Configure(EntityTypeBuilder<ItemPhoto> builder)
    {
        builder.ToTable("item_photos");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever(); // Guid gerado na entidade
        
        builder.Property(x => x.Url)
            .HasColumnName("url")
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.Order)
            .HasColumnName("order")
            .IsRequired();
        
        builder.Property(x => x.ItemId)
            .HasColumnName("item_id");
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        // Índices
        builder.HasIndex(x => new { x.ItemId, x.Order })
            .IsUnique()
            .HasDatabaseName("ix_item_photos_item_id_order");
        
        // Relacionamentos
        builder.HasOne(x => x.Item)
            .WithMany(x => x.Photos)
            .HasForeignKey(x => x.ItemId)
            .OnDelete(DeleteBehavior.Cascade);

        // Query Filter: filtra fotos de itens não deletados
        builder.HasQueryFilter(x => x.Item.DeletedAt == null);
    }
}
