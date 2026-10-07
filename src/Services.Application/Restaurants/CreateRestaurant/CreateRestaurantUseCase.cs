using Services.Domain.Restaurants;

namespace Services.Application.Restaurants.CreateRestaurant;

public sealed class CreateRestaurantUseCase(IRestaurantRepository repository)
{
    public async Task<RestaurantResponse> ExecuteAsync(CreateRestaurantRequest request, CancellationToken cancellationToken)
    {
        var restaurant = Restaurant.Create(request.Name, request.Description);
        foreach (var categoryName in request.Categories ?? [])
            restaurant.AddCategory(Domain.Categories.Category.Create(categoryName));
        await repository.AddAsync(restaurant, cancellationToken);
        return restaurant.ToResponse();
    }
}
