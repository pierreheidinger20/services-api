using System;
using Services.Application.Geolocation;
using Services.Domain.Restaurants;
using Services.Domain.ValueObject;
using Microsoft.Extensions.Logging;

namespace Services.Application.Restaurants.GetRestaurants;

public sealed class GetRestaurantsNearbyUseCase(
    ILogger<GetRestaurantsNearbyUseCase> logger,
    IRestaurantRepository restaurantRepository,
    IIpGeolocationService ipGeolocationService)
{

    public async Task<IReadOnlyList<RestaurantNearbyResponse>> ExecuteAsync(
        double latitude,
        double longitude,
        string ipAddress,
        CancellationToken cancellationToken)
    {
        Location? location;
        if ((latitude is < -90 or > 90) || (longitude is < -180 or > 180))
        {
            logger.LogWarning("Invalid location coordinates provided.");
            logger.LogInformation($"Attempting to determine location from IP address: {ipAddress}");
            location = await ipGeolocationService.GetLocationAsync(ipAddress, cancellationToken);
        }
        else
        {
            logger.LogInformation($"Using provided coordinates: Latitude={latitude}, Longitude={longitude}");
            location = new Location(latitude, longitude);
        }
        if (location is null)
        {
            throw new InvalidOperationException("Could not determine client location from IP address.");
        }
        var restaurants = (await restaurantRepository
            .GetNearbyAsync(location, cancellationToken)).Select(r => r.ToNearbyResponse()).ToArray(); ;
        logger.LogInformation($"Found {restaurants.Length} nearby restaurants for location: Latitude={location.Latitude}, Longitude={location.Longitude}");
        return restaurants;
    }

}
