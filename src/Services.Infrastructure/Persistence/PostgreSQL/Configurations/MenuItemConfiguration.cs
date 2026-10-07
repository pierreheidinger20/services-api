using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Restaurants;

namespace Services.Infrastructure.Persistence.PostgreSQL.Configurations;

public sealed class MenuItemConfiguration : IEntityTypeConfiguration<MenuItem>
{
    public void Configure(EntityTypeBuilder<MenuItem> builder)
    {
        builder.ToTable("menu_items");
        builder.HasKey(item => item.Id);
        builder.Property(item => item.Id).UseIdentityByDefaultColumn();
        builder.Property(item => item.PublicId).HasColumnName("public_id").IsRequired();
        builder.HasIndex(item => item.PublicId).IsUnique();
        builder.Property(item => item.Name).HasMaxLength(200).IsRequired();
        builder.Property(item => item.Description).HasMaxLength(2000).IsRequired();
        builder.Property(item => item.Price).HasPrecision(10, 2).IsRequired();
        builder.Property(item => item.ImageUrl).HasMaxLength(2048).IsRequired();
        builder.HasOne<Restaurant>().WithMany().HasForeignKey(item => item.RestaurantId).OnDelete(DeleteBehavior.Cascade);
    }
}
