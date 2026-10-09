using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using Services.Domain.Restaurants;
using Services.Domain.ValueObject;

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

    public async Task<IReadOnlyList<NearbyRestaurant>> GetNearbyAsync(Domain.ValueObject.Location location, CancellationToken cancellationToken)
    {
        if (location is null)
        {
            throw new ArgumentNullException(nameof(location));
        }
        int Wgs84 = 4326;
        Point point = new Point(
            location.Longitude,
            location.Latitude)
        {
            SRID = Wgs84
        };
        var query = dbContext.Restaurants
            .Where(r => r.Location.Distance(point) <= r.DeliveryRadiusMeters)
            .OrderBy(r => r.Location.Distance(point))
            .Select(r => new NearbyRestaurant(
                r,
                r.Location.Distance(point)));
        return await query.ToListAsync(cancellationToken);
    }
}
