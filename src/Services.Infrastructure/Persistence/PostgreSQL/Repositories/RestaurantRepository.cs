using Microsoft.EntityFrameworkCore;
using Services.Domain.Restaurants;

namespace Services.Infrastructure.Persistence.PostgreSQL.Repositories;

public sealed class RestaurantRepository(AppDbContext dbContext) : IRestaurantRepository
{
    public Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => dbContext.Restaurants.AsNoTracking().Include(r => r.Categories).SingleOrDefaultAsync(r => r.PublicId == id, cancellationToken);

    public async Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken)
        => await dbContext.Restaurants.AsNoTracking().Include(r => r.Categories).OrderBy(r => r.Name).ToListAsync(cancellationToken);

    public async Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken)
    {
        await dbContext.Restaurants.AddAsync(restaurant, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}
