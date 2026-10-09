using System;
using Services.Domain.ValueObject;

namespace Services.Application.Geolocation;

public interface IIpGeolocationService
{
    Task<Location?> GetLocationAsync(
            string ip,
            CancellationToken cancellationToken);
}
