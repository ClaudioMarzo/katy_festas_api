using KatyFestas.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KatyFestas.Infrastructure.Persistence.Configurations;

public class EstimateConfiguration : IEntityTypeConfiguration<Estimate>
{
    public void Configure(EntityTypeBuilder<Estimate> builder)
    {
        builder.ToTable("estimates");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id)
            .HasColumnName("id")
            .ValueGeneratedNever();
        
        builder.Property(x => x.StoreId)
            .HasColumnName("store_id");
        
        builder.Property(x => x.CustomerName)
            .HasColumnName("customer_name")
            .IsRequired()
            .HasMaxLength(200);
        
        builder.Property(x => x.CustomerEmail)
            .HasColumnName("customer_email")
            .HasMaxLength(200);
        
        builder.Property(x => x.CustomerPhone)
            .HasColumnName("customer_phone")
            .IsRequired()
            .HasMaxLength(20);
        
        builder.Property(x => x.EventDescription)
            .HasColumnName("event_description")
            .IsRequired()
            .HasMaxLength(500);
        
        builder.Property(x => x.EventForecastDate)
            .HasColumnName("event_forecast_date")
            .IsRequired();
        
        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();
        
        // Índices
        builder.HasIndex(x => x.StoreId)
            .HasDatabaseName("ix_estimates_store_id");
        
        builder.HasIndex(x => x.EventForecastDate)
            .HasDatabaseName("ix_estimates_event_forecast_date");
        
        // Relacionamentos
        builder.HasOne(x => x.Store)
            .WithMany(x => x.Estimates)
            .HasForeignKey(x => x.StoreId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasMany(x => x.Items)
            .WithOne(x => x.Estimate)
            .HasForeignKey(x => x.EstimateId)
            .OnDelete(DeleteBehavior.Cascade);

        // Query Filter: filtra estimates de lojas não deletadas
        builder.HasQueryFilter(x => x.Store.DeletedAt == null);
    }
}
