using Services.Domain.Restaurants;

namespace Services.Application.Restaurants;

internal static class RestaurantMapping
{
    internal static RestaurantResponse ToResponse(this Restaurant restaurant)
        => new(restaurant.PublicId, restaurant.Name, restaurant.Categories.Select(category => category.PublicId).ToArray(),
            restaurant.Rating, restaurant.DeliveryTime, restaurant.DeliveryFee, restaurant.ImageUrl, restaurant.Tag,
            restaurant.Description, restaurant.CreatedAt, restaurant.UpdatedAt);

    
    internal static RestaurantNearbyResponse ToNearbyResponse(this NearbyRestaurant nearbyRestaurant)
        => new(nearbyRestaurant.Restaurant.PublicId, nearbyRestaurant.Restaurant.Name, nearbyRestaurant.Restaurant.Categories.Select(category => category.PublicId).ToArray(),
            nearbyRestaurant.Restaurant.Rating, nearbyRestaurant.Restaurant.DeliveryTime, nearbyRestaurant.Restaurant.DeliveryFee, nearbyRestaurant.Restaurant.ImageUrl, nearbyRestaurant.Restaurant.Tag,
            nearbyRestaurant.Restaurant.Description, nearbyRestaurant.Restaurant.CreatedAt, nearbyRestaurant.Restaurant.UpdatedAt, nearbyRestaurant.Distance);
}
