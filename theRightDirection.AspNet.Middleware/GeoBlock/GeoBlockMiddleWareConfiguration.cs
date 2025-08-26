using MaxMind.Db;
using Serilog;
using System.Net;

namespace theRightDirection.AspNet.Middleware.GeoBlock;

public class GeoBlockMiddleWareConfiguration
{
    private readonly Reader _reader;
    private readonly string[] _isoCountryNames;

    public GeoBlockMiddleWareConfiguration(string maxMindDatabaseFilePath, List<string> isoCountryNames = null)
    {
        if (File.Exists(maxMindDatabaseFilePath))
        {
            _reader = new Reader(maxMindDatabaseFilePath, FileAccessMode.Memory);
        }
        // NL is Nederland
        // DE is Duitsland
        if (isoCountryNames == null)
        {
            isoCountryNames = new List<string> { "NL", "DE" };
        }
        _isoCountryNames = isoCountryNames.ToArray();
    }

    public bool IPIsFromAllowedCountry(IPAddress ip)
    {
        var data = Lookup(ip);
        var countryCode = GetCountryCode(data);
        Log.Logger.Here().Debug($"ISO country code for IP: {countryCode}");
        if (countryCode.HasNoText() || !_isoCountryNames.Contains(countryCode))
        {
            return false;
        }
        return true;
    }

    private string GetCountryCode(Dictionary<string, object> data)
    {
        if (data != null)
        {
            var cnty = data["country"] as Dictionary<string, object>;
            Log.Logger.Here().Debug((string)cnty["iso_code"]);
            return (string)cnty["iso_code"];
        }
        return string.Empty;
    }

    private Dictionary<string, object> Lookup(IPAddress ip)
    {
        if (_reader == null)
        {
            return new Dictionary<string, object>();
        }
        return _reader?.Find<Dictionary<string, object>>(ip);
    }
}