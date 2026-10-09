using System;

namespace Services.Domain.Restaurants;

public sealed record NearbyRestaurant(
    Restaurant Restaurant,
    double Distance
);