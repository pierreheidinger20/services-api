namespace Services.Domain.Restaurants;

public sealed class MenuItem
{
    private MenuItem(long restaurantId, string name, string description, decimal price, string imageUrl)
    {
        RestaurantId = restaurantId;
        Name = name;
        Description = description;
        Price = price;
        ImageUrl = imageUrl;
    }

    public long Id { get; private set; }
    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public long RestaurantId { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public string ImageUrl { get; private set; }

    public static MenuItem Create(long restaurantId, string name, string description, decimal price, string imageUrl)
    {
        if (restaurantId <= 0) throw new ArgumentOutOfRangeException(nameof(restaurantId));
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        ArgumentException.ThrowIfNullOrWhiteSpace(description);
        ArgumentException.ThrowIfNullOrWhiteSpace(imageUrl);
        if (price < 0) throw new ArgumentOutOfRangeException(nameof(price));
        return new MenuItem(restaurantId, name.Trim(), description.Trim(), price, imageUrl.Trim());
    }
}
