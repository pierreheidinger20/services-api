using Services.Domain.Categories;

namespace Services.Domain.Restaurants;

public sealed class Restaurant
{
    private readonly List<Category> _categories = [];
    private readonly List<MenuItem> _menus = [];

    private Restaurant(string name, decimal rating, string deliveryTime, decimal deliveryFee, string imageUrl, string? tag, string? description, DateTimeOffset createdAt)
    {
        Name = name;
        Description = description;
        Rating = rating;
        DeliveryTime = deliveryTime;
        DeliveryFee = deliveryFee;
        ImageUrl = imageUrl;
        Tag = tag;
        CreatedAt = createdAt;
    }

    public long Id { get; private set; }
    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public IReadOnlyCollection<Category> Categories => _categories;
    public IReadOnlyCollection<MenuItem> Menus => _menus;
    public decimal Rating { get; private set; }
    public string DeliveryTime { get; private set; } = string.Empty;
    public decimal DeliveryFee { get; private set; }
    public string ImageUrl { get; private set; } = string.Empty;
    public string? Tag { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public void AddCategory(Category category)
    {
        ArgumentNullException.ThrowIfNull(category);
        if (_categories.Any(existing => existing.Name.Equals(category.Name, StringComparison.OrdinalIgnoreCase))) return;
        _categories.Add(category);
    }

    public static Restaurant Create(string name, string? description, DateTimeOffset? now = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var normalizedName = name.Trim();
        if (normalizedName.Length > 200) throw new ArgumentException("Name cannot exceed 200 characters.", nameof(name));
        if (description?.Length > 2000) throw new ArgumentException("Description cannot exceed 2000 characters.", nameof(description));
        return new Restaurant(normalizedName, 0, string.Empty, 0, string.Empty, null, description?.Trim(), now ?? DateTimeOffset.UtcNow);
    }

    public static Restaurant Create(string name, string category, decimal rating, string deliveryTime, decimal deliveryFee, string imageUrl, string? tag = null, string? description = null, DateTimeOffset? now = null)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var normalizedName = name.Trim();
        if (normalizedName.Length > 200) throw new ArgumentException("Name cannot exceed 200 characters.", nameof(name));
        if (description?.Length > 2000) throw new ArgumentException("Description cannot exceed 2000 characters.", nameof(description));
        ArgumentException.ThrowIfNullOrWhiteSpace(deliveryTime);
        ArgumentException.ThrowIfNullOrWhiteSpace(imageUrl);
        if (rating is < 0 or > 5) throw new ArgumentOutOfRangeException(nameof(rating));
        if (deliveryFee < 0) throw new ArgumentOutOfRangeException(nameof(deliveryFee));
        var restaurant = new Restaurant(normalizedName, rating, deliveryTime.Trim(), deliveryFee,
            imageUrl.Trim(), string.IsNullOrWhiteSpace(tag) ? null : tag.Trim(), description?.Trim(), now ?? DateTimeOffset.UtcNow);
        if (!string.IsNullOrWhiteSpace(category)) restaurant.AddCategory(Category.Create(category));
        return restaurant;
    }
}
