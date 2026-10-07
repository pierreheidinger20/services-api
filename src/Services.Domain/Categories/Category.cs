namespace Services.Domain.Categories;

public sealed class Category
{
    private Category(string name) { Name = name; }

    public long Id { get; private set; }
    public Guid PublicId { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }

    public static Category Create(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
        var normalizedName = name.Trim();
        if (normalizedName.Length > 100) throw new ArgumentException("Name cannot exceed 100 characters.", nameof(name));
        return new Category(normalizedName);
    }
}
