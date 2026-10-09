using Microsoft.Extensions.DependencyInjection;
using Services.Application.Categories.GetCategories;
using Services.Application.Restaurants.CreateRestaurant;
using Services.Application.Restaurants.GetRestaurant;
using Services.Application.Restaurants.GetRestaurants;

namespace Services.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateRestaurantUseCase>();
        services.AddScoped<GetRestaurantUseCase>();
        services.AddScoped<GetRestaurantsUseCase>();
        services.AddScoped<GetCategoriesUseCase>();
        services.AddScoped<GetRestaurantsNearbyUseCase>();
        return services;
    }
}
