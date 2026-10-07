using Microsoft.EntityFrameworkCore;
using Services.Domain.Categories;

namespace Services.Infrastructure.Persistence.PostgreSQL.Repositories;

public sealed class CategoryRepository(AppDbContext dbContext) : ICategoryRepository
{
    public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken)
        => await dbContext.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync(cancellationToken);
}
