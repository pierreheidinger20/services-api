namespace Services.Api.Extensions;

public static class HttpContextExtensions
{
    public static string? GetClientIp(this HttpContext context)
    {
        return context.Request.Headers["X-Real-Ip"]
            .FirstOrDefault()
            ?? context.Connection.RemoteIpAddress?.ToString();
    }
}