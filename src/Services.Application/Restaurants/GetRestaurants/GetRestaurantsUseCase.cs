using Services.Domain.Restaurants;

namespace Services.Application.Restaurants.GetRestaurants;

public sealed class GetRestaurantsUseCase(IRestaurantRepository repository)
{
    public async Task<IReadOnlyList<RestaurantResponse>> ExecuteAsync(CancellationToken cancellationToken)
        => (await repository.GetAllAsync(cancellationToken)).Select(r => r.ToResponse()).ToArray();
}
