using Microsoft.AspNetCore.Mvc;
using Services.Application.Restaurants.CreateRestaurant;
using Services.Application.Restaurants.GetRestaurant;
using Services.Application.Restaurants.GetRestaurants;

namespace Services.Api.Controllers;

[ApiController]
[Route("api/v1/restaurants")]
public sealed class RestaurantsController(
    CreateRestaurantUseCase createRestaurant,
    GetRestaurantUseCase getRestaurant,
    GetRestaurantsUseCase getRestaurants) : ControllerBase
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
        => Ok(await getRestaurants.ExecuteAsync(cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
    {
        var restaurant = await getRestaurant.ExecuteAsync(id, cancellationToken);
        return restaurant is null ? NotFound() : Ok(restaurant);
    }
}
