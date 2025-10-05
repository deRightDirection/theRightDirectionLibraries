using MaxMind.Db;
using Serilog;
using System.Net;

namespace theRightDirection.AspNet.Middleware.GeoBlock;

public class GeoBlockMiddleWareConfiguration
{
    private readonly Reader _reader;
    private readonly string[] _isoCountryNames;
    private readonly bool _isValid;

    public GeoBlockMiddleWareConfiguration(string maxMindDatabaseFilePath, List<string> isoCountryNames = null)
    {
        if (File.Exists(maxMindDatabaseFilePath))
        {
            _reader = new Reader(maxMindDatabaseFilePath, FileAccessMode.Memory);
            _isValid = true;
        }
        // NL is Nederland
        // DE is Duitsland
        // SE is Zweden
        if (isoCountryNames == null)
        {
            // TODO 26-08-2025 voor KLIC Genius dit flexibel maken
            isoCountryNames = new List<string> { "NL", "DE", "SE" };
        }
        _isoCountryNames = isoCountryNames.ToArray();
    }

    public bool IPIsFromAllowedCountry(IPAddress ip)
    {
#if DEBUG
        return true;
#endif
        if (!_isValid)
        {
            Log.Logger.Here().Warning($"there is no MaxMind database file found, all IP-addresses are allowed");
            return true;
        }
        var data = Lookup(ip);
        var countryCode = GetCountryCode(data);
        if (countryCode.HasNoText() || !_isoCountryNames.Contains(countryCode))
        {
            Log.Logger.Here().Debug($"ip denied: {countryCode} {ip}");
            return false;
        }
        Log.Logger.Here().Debug($"ip allowed: {countryCode} {ip}");
        return true;
    }

    private string GetCountryCode(Dictionary<string, object> data)
    {
        if (data != null)
        {
            var cnty = data["country"] as Dictionary<string, object>;
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