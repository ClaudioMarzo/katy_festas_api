using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Nome da tabela
        builder.ToTable("categories");
        
        // Chave primária
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever(); // Guid gerado na entidade
        
        builder.Property(x => x.StoreId)
            .HasColumnName("store_id");
        
        builder.Property(x => x.Name)
            .HasColumnName("name") // nome da coluna
            .IsRequired() // not nul
            .HasMaxLength(100); // limite de caracteres

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
        builder.HasIndex(x => new { x.StoreId, x.Name })
            .IsUnique()
            .HasDatabaseName("ix_categories_store_id_name");
        
        // Relacionamentos
        builder.HasOne(x => x.Store)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Category)
            .HasForeignKey(x => x.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
