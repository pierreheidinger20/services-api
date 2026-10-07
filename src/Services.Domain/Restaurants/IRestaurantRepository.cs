namespace Services.Domain.Restaurants;

public interface IRestaurantRepository
{
    Task<Restaurant?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<Restaurant>> GetAllAsync(CancellationToken cancellationToken);
    Task AddAsync(Restaurant restaurant, CancellationToken cancellationToken);
}
