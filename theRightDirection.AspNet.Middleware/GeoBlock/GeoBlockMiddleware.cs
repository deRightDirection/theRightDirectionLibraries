using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Serilog;

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

    public async Task Invoke(HttpContext context)
    {
        var ipAddres = context.Connection.RemoteIpAddress?.ToString();
        Log.Logger.Here().Debug(context.Connection.RemoteIpAddress.ToString());
        var ipAllowed = _cache.GetOrCreate(ipAddres, x => _geo.IPIsFromAllowedCountry(context.Connection.RemoteIpAddress));
        if (ipAllowed)
        {
            await _next.Invoke(context);
        }
    }
}