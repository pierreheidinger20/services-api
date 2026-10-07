using Services.Domain.Restaurants;

namespace Services.Application.Restaurants;

internal static class RestaurantMapping
{
    internal static RestaurantResponse ToResponse(this Restaurant restaurant)
        => new(restaurant.PublicId, restaurant.Name, restaurant.Categories.Select(category => category.PublicId).ToArray(),
            restaurant.Rating, restaurant.DeliveryTime, restaurant.DeliveryFee, restaurant.ImageUrl, restaurant.Tag,
            restaurant.Description, restaurant.CreatedAt, restaurant.UpdatedAt);
}
