using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
        
        builder.Property(x => x.Name)
            .HasColumnName("name")
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.Email)
            .HasColumnName("email")
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.PasswordHash)
            .HasColumnName("password_hash")
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.IsActive)
            .HasColumnName("is_active")
            .IsRequired()
            .HasDefaultValue(true);
        
        builder.Property(x => x.StoreId)
            .HasColumnName("store_id");
        
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
        builder.HasIndex(x => x.Email)
            .IsUnique()
            .HasDatabaseName("ix_users_email")
            .HasFilter("deleted_at IS NULL");
        
        builder.HasIndex(x => x.StoreId)
            .HasDatabaseName("ix_users_store_id");
        
        // Relacionamentos
        builder.HasOne(x => x.Store)
            .WithMany(x => x.Users)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
