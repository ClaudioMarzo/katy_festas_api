using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class StoreConfiguration : IEntityTypeConfiguration<Store>
{
    public void Configure(EntityTypeBuilder<Store> builder)
    {
        builder.ToTable("stores");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever(); // Guid gerado na entidade
        
        builder.Property(x => x.StoreId)
            .HasColumnName("store_id")
            .IsRequired()
            .ValueGeneratedOnAdd()
            .UseIdentityColumn(); // Auto-incremento para ID numérico
        
        builder.HasIndex(x => x.StoreId)
            .IsUnique();
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.WhatsappPhone)
            .HasColumnName("whatsapp_phone")
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at");
        
        builder.Property(x => x.DeletedAt)
            .HasColumnName("deleted_at");
        
        // Soft Delete Filter
        builder.HasQueryFilter(x => x.DeletedAt == null);
        
        // Relacionamentos
        builder.HasMany(x => x.Categories)
            .WithOne(x => x.Store)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Store)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
