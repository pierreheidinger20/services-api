using Microsoft.AspNetCore.Mvc;
using Services.Application.Restaurants.CreateRestaurant;
using Services.Application.Restaurants.GetRestaurant;
using Services.Application.Restaurants.GetRestaurants;
using Services.Api.Extensions;
using Services.Application.Geolocation;

namespace Services.Api.Controllers;

[ApiController]
[Route("api/v1/restaurants")]
public sealed class RestaurantsController(
    ILogger<RestaurantsController> logger,
    IIpGeolocationService ipGeolocationService,
    CreateRestaurantUseCase createRestaurant,
    GetRestaurantUseCase getRestaurant,
    GetRestaurantsUseCase getRestaurants,
    GetRestaurantsNearbyUseCase getNearbyRestaurants) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(Services.Application.Restaurants.RestaurantResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantRequest request, CancellationToken cancellationToken)
    {
        var restaurant = await createRestaurant.ExecuteAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = restaurant.PublicId }, restaurant);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        logger.LogInformation("Received request to get all restaurants.");
        var ip = HttpContext.GetClientIp();
        logger.LogInformation($"Request from IP: {ip}");
        return Ok(await getRestaurants.ExecuteAsync(cancellationToken));
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var restaurant = await getRestaurant.ExecuteAsync(id, cancellationToken);
        return restaurant is null ? NotFound() : Ok(restaurant);
    }

    [HttpGet("nearby")]
    public async Task<IActionResult> GetAllNearby([FromQuery] double lat,[FromQuery] double lng, CancellationToken cancellationToken)
    {
        logger.LogInformation("Received request to get all restaurants.");
        string? ip = HttpContext.GetClientIp();
        if (ip is null)
        {
            logger.LogWarning("Could not determine client IP address.");
            return BadRequest("Could not determine client IP address.");
        }
        logger.LogInformation($"Request from IP: {ip}");
        return Ok(await getNearbyRestaurants.ExecuteAsync(lat, lng, ip, cancellationToken));
    }
}
