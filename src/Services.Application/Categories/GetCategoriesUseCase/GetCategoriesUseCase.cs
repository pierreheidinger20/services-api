using Services.Domain.Categories;

namespace Services.Application.Categories.GetCategories;

public sealed class GetCategoriesUseCase(ICategoryRepository repository)
{
    public async Task<IReadOnlyList<CategoryResponse>> ExecuteAsync(CancellationToken cancellationToken)
        => (await repository.GetAllAsync(cancellationToken)).Select(c => c.ToResponse()).ToArray();
}
