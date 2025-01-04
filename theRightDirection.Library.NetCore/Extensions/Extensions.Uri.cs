using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Web;

namespace theRightDirection;

public static partial class Extensions
{
    /// <summary>
    /// determine of the URI is coming from ArcGIS Online or ArcGIS Enterprise
    /// </summary>
    public static bool IsArcGISOnline(this Uri uri)
    {
        if (uri == null)
        {
            return false;
        }
        var absoluteUri = uri.AbsoluteUri;
        return absoluteUri.Contains("maps.arcgis.com", StringComparison.InvariantCultureIgnoreCase) || absoluteUri.Contains("www.arcgis.com", StringComparison.InvariantCultureIgnoreCase);
    }

    /// <summary>
    /// rewrite the uri, strip the values of queryparameters to a maximum of five characters<br/>
    /// current supported query paramaters names:<br/>
    /// - code<br/>
    /// - code_verifier<br/>
    /// - token<br/>
    /// - passsword<br/>
    /// - access_token<br/>
    /// - refresh_token<br/>
    /// - auth_token<br/>
    /// - clientid
    /// </summary>
    public static string RemoveSensitiveDataFromUri(this Uri uri)
    {
        var sensitiveParameterNames = new List<string> { "code", "code_verifier", "token", "password", "access_token", "refresh_token", "auth_token", "clientid" };
        if (uri.IsInvalidAbsoluteUri())
        {
            return string.Empty;
        }
        var queryParameters = HttpUtility.ParseQueryString(uri.Query);
        var parameters = new List<string>();
        foreach (string parameter in queryParameters)
        {
            var value = queryParameters[parameter];
            if (sensitiveParameterNames.Contains(parameter))
            {
                if (value.HasText() && value.Length > 5)
                {
                    value = value.Substring(0, 5);
                }
            }
            parameters.Add($"{parameter}={value}");
        }
        return $"{uri.GetLeftPart(UriPartial.Path)}?{string.Join("&", parameters)}".Trim();
    }

    /// <summary>
    /// check if the uri != null and is absolute uri as well
    /// </summary>
    public static bool IsValidAbsoluteUri(this Uri uri)
    {
        if (uri == null)
        {
            return false;
        }
        return uri.IsAbsoluteUri;
    }

    /// <summary>
    /// check if the uri == null and is not absolute uri as well
    /// </summary>
    public static bool IsInvalidAbsoluteUri(this Uri uri)
    {
        if (uri == null)
        {
            Log.Logger.Here().Debug($"uri is relative or null: '{uri}'");
            return true;
        }
        return !uri.IsAbsoluteUri;
    }

    /// <summary>
    /// check if two URI's are equal, only applies to absolute uri's
    /// </summary>
    public static bool IsEqual(this Uri uri, Uri otherUri)
    {
        // TODO AnyDiff gebruiken
        if (!uri.IsAbsoluteUri || !otherUri.IsAbsoluteUri)
        {
            return false;
        }
        var uri1 = uri.AbsoluteUri.ToLowerInvariant();
        var uri2 = otherUri.AbsoluteUri.ToLowerInvariant();
        if (uri1.EndsWith("/"))
        {
            uri1 = uri1.RemoveSpecialCharacterAtTheEndFromString();
        }
        if (uri2.EndsWith("/"))
        {
            uri2 = uri1.RemoveSpecialCharacterAtTheEndFromString();
        }
        return uri1.Equals(uri2, StringComparison.Ordinal);
    }

    /// <summary>
    /// open the uri in a new browser by process.start-call
    /// </summary>
    /// <param name="uri"></param>
    public static void OpenInBrowser(this Uri uri)
    {
        var url = uri.AbsoluteUri;
        try
        {
            Process.Start(url);
        }
        catch
        {
            // hack because of this: https://github.com/dotnet/corefx/issues/10361
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Process.Start("xdg-open", url);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                Process.Start("open", url);
            }
            else
            {
                throw;
            }
        }
    }

    /// <summary>
    /// rewrite a http-url to https
    /// </summary>
    public static Uri RewriteUriToSecureHttps(this Uri uri)
    {
        return new UriBuilder(uri)
        {
            Scheme = Uri.UriSchemeHttps,
            Port = uri.IsDefaultPort ? -1 : uri.Port
        }.Uri;
    }
}