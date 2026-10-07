using Services.Domain.Restaurants;

namespace Services.UnitTests;

public sealed class RestaurantTests
{
    [Fact]
    public void Create_trims_values_and_sets_timestamps()
    {
        var now = DateTimeOffset.UtcNow;
        var restaurant = Restaurant.Create("  Cafe  ", "  Nice  ", now);
        Assert.Equal("Cafe", restaurant.Name);
        Assert.Equal("Nice", restaurant.Description);
        Assert.Equal(now, restaurant.CreatedAt);
        Assert.Null(restaurant.UpdatedAt);
        Assert.NotEqual(Guid.Empty, restaurant.PublicId);
    }

    [Theory]
    [InlineData("")]
    [InlineData("  ")]
    public void Create_rejects_blank_name(string name)
        => Assert.Throws<ArgumentException>(() => Restaurant.Create(name, null));

    [Fact]
    public void Create_rejects_name_longer_than_200_characters()
        => Assert.Throws<ArgumentException>(() => Restaurant.Create(new string('x', 201), null));
}
