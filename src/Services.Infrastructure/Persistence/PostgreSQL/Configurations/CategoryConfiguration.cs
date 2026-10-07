using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Services.Domain.Categories;

namespace Services.Infrastructure.Persistence.PostgreSQL.Configurations;

public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");
        builder.HasKey(category => category.Id);
        builder.Property(category => category.Id).UseIdentityByDefaultColumn();
        builder.Property(category => category.PublicId).HasColumnName("public_id").IsRequired();
        builder.HasIndex(category => category.PublicId).IsUnique();
        builder.Property(category => category.Name).HasMaxLength(100).IsRequired();
        builder.HasIndex(category => category.Name).IsUnique();
    }
}
