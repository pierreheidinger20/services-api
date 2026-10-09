using System.Net.Http.Json;
using Services.Application.Geolocation;
using Services.Domain.ValueObject;


namespace Services.Infrastructure.Geolocation;

public sealed class IpGeolocationService(
    HttpClient httpClient) : IIpGeolocationService
{
    public async Task<Location?> GetLocationAsync(
        string ip,
        CancellationToken cancellationToken)
    {
        var response = await httpClient.GetFromJsonAsync<IpApiResponse>(
            $"http://ip-api.com/json/{ip}",
            cancellationToken);

        if (response is null)
            return null;

        return new Location(
            response.Latitude,
            response.Longitude
        );
    }
}