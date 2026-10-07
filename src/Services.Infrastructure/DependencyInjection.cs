using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Domain.Categories;
using Services.Domain.Restaurants;
using Services.Infrastructure.Persistence.PostgreSQL;
using Services.Infrastructure.Persistence.PostgreSQL.Repositories;

namespace Services.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("PostgreSQL")));
        
        services.AddScoped<IRestaurantRepository, RestaurantRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}
