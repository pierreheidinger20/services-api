using System.ComponentModel.DataAnnotations;

namespace Services.Application.Restaurants.CreateRestaurant;

public sealed record CreateRestaurantRequest(
    [property: Required, StringLength(200, MinimumLength = 1), RegularExpression(".*\\S.*")] string Name,
    [property: StringLength(2000)] string? Description,
    IReadOnlyList<string>? Categories = null);
