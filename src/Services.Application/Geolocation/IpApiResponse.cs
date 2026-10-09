namespace Services.Application.Geolocation;

public sealed record IpApiResponse(
    string Country,
    string CountryCode,
    string Region,
    string RegionName,
    string City,
    string Zip,
    double Lat,
    double Lon,
    string Timezone,
    string Isp,
    string Org,
    string As
);