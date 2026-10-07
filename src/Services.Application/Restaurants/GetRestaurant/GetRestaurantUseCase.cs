using Services.Domain.Restaurants;

namespace Services.Application.Restaurants.GetRestaurant;

public sealed class GetRestaurantUseCase(IRestaurantRepository repository)
{
    public async Task<RestaurantResponse?> ExecuteAsync(Guid id, CancellationToken cancellationToken)
        => (await repository.GetByIdAsync(id, cancellationToken))?.ToResponse();
}
