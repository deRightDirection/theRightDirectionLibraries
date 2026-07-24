using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace theRightDirection.Http;

public class HttpLoggingService(HttpMessageHandler innerHandler = null) : DelegatingHandler(innerHandler ?? new HttpClientHandler())
{
    private readonly bool _showMinimalPostInformation = true;

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var req = request;
        var id = Guid.NewGuid().ToString();
        var query = StripPathAndQuery(req.RequestUri?.PathAndQuery);
        Log.Logger.Http(id, "request").Debug($"{req.Method} Host: {req.RequestUri.Scheme}://{req.RequestUri.Host} {query}");

        foreach (var header in req.Headers)
        {
            Log.Logger.Http(id, "request").Debug($"{header.Key}: {string.Join(", ", header.Value)}");
        }

        if (req.Content != null)
        {
            foreach (var header in req.Content.Headers)
            {
                Log.Logger.Http(id, "request").Debug($"{header.Key}: {string.Join(", ", header.Value)}");
            }

            if (req.Content is StringContent || IsTextBasedContentType(req.Headers) || IsTextBasedContentType(req.Content.Headers))
            {
                var result = await req.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                if (result.HasText())
                {
                    result = result.RemoveSensitiveData();
                    result = result.Minifiy();
                    Log.Logger.Http(id, "request").Debug($"{result}");
                }
            }
        }

        var start = DateTime.Now;
        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var resp = response;
        if (!_showMinimalPostInformation)
        {
            Log.Logger.Http(id, "response").Debug($"{req.RequestUri.Scheme.ToUpper()}/{resp.Version} {(int)resp.StatusCode} {resp.ReasonPhrase}");
            foreach (var header in resp.Headers)
            {
                Log.Logger.Http(id, "response").Debug($"{header.Key}: {string.Join(", ", header.Value)}");
            }
        }

        if (resp.Content != null)
        {
            if (!_showMinimalPostInformation)
            {
                foreach (var header in resp.Content.Headers)
                {
                    Log.Logger.Http(id, "response").Debug($"{header.Key}: {string.Join(", ", header.Value)}");
                }
            }

            if (resp.Content is StringContent || this.IsTextBasedContentType(resp.Headers) || IsTextBasedContentType(resp.Content.Headers))
            {
                var result = await resp.Content.ReadAsStringAsync(cancellationToken);
                if (result.HasText())
                {
                    result = result.RemoveSensitiveData();
                    result = result.Minifiy();
                    Log.Logger.Http(id, "response").Debug(result);
                }
            }
        }
        var end = DateTime.Now;
        Log.Logger.Http(id, "response").Debug($"Duration: {end - start}");
        return response;
    }

    readonly string[] types = new[] { "html", "text", "xml", "json", "txt", "x-www-form-urlencoded" };
    private readonly string[] queryParameterNamesToStrip = new[] { "token", "apikey", "fmetoken", "clientid" };

    bool IsTextBasedContentType(HttpHeaders headers)
    {
        IEnumerable<string> values;
        if (!headers.TryGetValues("Content-Type", out values))
        {
            return false;
        }
        var header = string.Join(" ", values).ToLowerInvariant();
        return types.Any(t => header.Contains(t));
    }

#if DEBUG
    internal string StripPathAndQuery(string pathAndQuery, int numberToStrip = 3)
#else
    private string StripPathAndQuery(string pathAndQuery, int numberToStrip = 3)
#endif
    {
        if (pathAndQuery == null || pathAndQuery.HasNoText())
        {
            return string.Empty;
        }
        var parts = pathAndQuery.Trim().Split("?", StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
        {
            return pathAndQuery;
        }
        var query = parts[1];
        var queryParameters = query.Split("&", StringSplitOptions.RemoveEmptyEntries);
        var newParameters = new List<string>();
        foreach (var queryParameter in queryParameters)
        {
            var parameterParts = queryParameter.Split("=");
            if (parameterParts.Length != 2)
            {
                newParameters.Add(queryParameter);
            }
            var name = parameterParts[0].ToLowerInvariant();
            if (queryParameterNamesToStrip.Contains(name))
            {
                var value = parameterParts[1];
                var newValue = value.ToStripForLogging(numberToStrip);
                newParameters.Add($"{name}={newValue}");
            }
            else
            {
                newParameters.Add(queryParameter);
            }
        }
        var newQuery = string.Join("&", newParameters);
        return $"{parts[0]}?{newQuery}";
    }
}