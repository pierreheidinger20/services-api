namespace Services.Application.Restaurants;

public sealed record RestaurantNearbyResponse(
    Guid PublicId,
    string Name,
    IReadOnlyList<Guid> Categories,
    decimal Rating,
    string DeliveryTime,
    decimal DeliveryFee,
    string ImageUrl,
    string? Tag,
    string? Description,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt,
    double Distance);
