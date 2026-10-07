using Services.Domain.Categories;

namespace Services.Application.Categories;

internal static class CategoryMapping
{
    internal static CategoryResponse ToResponse(this Category category)
        => new(category.PublicId, category.Name);
}
