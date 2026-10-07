using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Services.Infrastructure.Persistence.PostgreSQL;

namespace Services.IntegrationTests;

public sealed class RestaurantsEndpointTests : IClassFixture<RestaurantsApiFactory>
{
    private readonly HttpClient _client;
    public RestaurantsEndpointTests(RestaurantsApiFactory factory) => _client = factory.CreateClient();

    [Fact]
    public async Task Post_creates_restaurant_and_get_by_id_returns_it()
    {
        var response = await _client.PostAsJsonAsync("/api/v1/restaurants", new { name = "Cafe Central", description = "Coffee" });
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        var created = await response.Content.ReadFromJsonAsync<RestaurantDto>();
        Assert.NotNull(created);
        var fetched = await _client.GetAsync($"/api/v1/restaurants/{created.PublicId}");
        Assert.Equal(HttpStatusCode.OK, fetched.StatusCode);
        Assert.Equal("Cafe Central", (await fetched.Content.ReadFromJsonAsync<RestaurantDto>())?.Name);
    }

    [Fact]
    public async Task Get_all_returns_created_restaurants()
    {
        await _client.PostAsJsonAsync("/api/v1/restaurants", new { name = "Cafe North", description = (string?)null });
        var response = await _client.GetAsync("/api/v1/restaurants");
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(await response.Content.ReadFromJsonAsync<RestaurantDto[]>() ?? []);
    }

    [Fact]
    public async Task Get_unknown_id_returns_404()
        => Assert.Equal(HttpStatusCode.NotFound, (await _client.GetAsync($"/api/v1/restaurants/{Guid.NewGuid()}")).StatusCode);

    [Fact]
    public async Task Post_blank_name_returns_400()
        => Assert.Equal(HttpStatusCode.BadRequest, (await _client.PostAsJsonAsync("/api/v1/restaurants", new { name = "   " })).StatusCode);

    private sealed record RestaurantDto(Guid PublicId, string Name, IReadOnlyList<string> Categories, decimal Rating, string DeliveryTime, decimal DeliveryFee, string ImageUrl, string? Tag, string? Description, DateTimeOffset CreatedAt, DateTimeOffset? UpdatedAt);
}

public sealed class RestaurantsApiFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Testing");
        builder.ConfigureServices(services =>
        {
            services.RemoveAll<DbContextOptions<AppDbContext>>();
            services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("restaurants-tests"));
        });
    }
}
