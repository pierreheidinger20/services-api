using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Restaurants;

namespace Services.Infrastructure.Persistence.PostgreSQL.Configurations;

public sealed class RestaurantConfiguration : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.ToTable("restaurants");
        builder.HasKey(r => r.Id);
        builder.Property(r => r.Id).UseIdentityByDefaultColumn();
        builder.Property(r => r.PublicId).HasColumnName("public_id").IsRequired();
        builder.HasIndex(r => r.PublicId).IsUnique();
        builder.Property(r => r.Name).HasMaxLength(200).IsRequired();
        builder.Property(r => r.Description).HasMaxLength(2000);
        builder.Property(r => r.Rating).HasPrecision(2, 1).IsRequired();
        builder.Property(r => r.DeliveryTime).HasMaxLength(50).IsRequired();
        builder.Property(r => r.DeliveryFee).HasPrecision(10, 2).IsRequired();
        builder.Property(r => r.ImageUrl).HasMaxLength(2048).IsRequired();
        builder.Property(r => r.Tag).HasMaxLength(100);
        builder.Property(r => r.CreatedAt).IsRequired();
        builder.HasMany(r => r.Categories).WithMany().UsingEntity(join => join.ToTable("restaurant_categories"));
        builder.HasMany(r => r.Menus).WithMany().UsingEntity(join => join.ToTable("restaurant_menus"));
    }
}
