using System.Text.Json.Serialization;

namespace Services.Infrastructure.Geolocation;

internal sealed record IpApiResponse(
    [property: JsonPropertyName("country")]
    string Country,

    [property: JsonPropertyName("countryCode")]
    string CountryCode,

    [property: JsonPropertyName("region")]
    string Region,

    [property: JsonPropertyName("regionName")]
    string RegionName,

    [property: JsonPropertyName("city")]
    string City,

    [property: JsonPropertyName("zip")]
    string Zip,

    [property: JsonPropertyName("lat")]
    double Latitude,

    [property: JsonPropertyName("lon")]
    double Longitude,

    [property: JsonPropertyName("timezone")]
    string Timezone,

    [property: JsonPropertyName("isp")]
    string Isp,

    [property: JsonPropertyName("org")]
    string Organization,

    [property: JsonPropertyName("as")]
    string AutonomousSystem
);