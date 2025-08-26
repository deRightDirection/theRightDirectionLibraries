using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace theRightDirection.AspNet.Middleware.GeoBlock;

public class GeoBlockMiddleware
{
    private readonly RequestDelegate _next;
    private readonly GeoBlockMiddleWareConfiguration _geo;
    private readonly IMemoryCache _cache;

    public GeoBlockMiddleware(RequestDelegate next, GeoBlockMiddleWareConfiguration geo, IMemoryCache cache)
    {
        _next = next;
        _geo = geo;
        _cache = cache;
    }

    public Task Invoke(HttpContext context)
    {
        var ipAddres = context.Connection.RemoteIpAddress?.ToString();
        var ipAllowed = _cache.GetOrCreate(ipAddres, x => _geo.IPIsFromAllowedCountry(context.Connection.RemoteIpAddress));
        if (!ipAllowed)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return Task.CompletedTask;
        }
        return _next.Invoke(context);
    }
}