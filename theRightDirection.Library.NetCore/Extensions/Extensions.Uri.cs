using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace theRightDirection
{
    public static partial class Extensions
    {
        /// <summary>
        /// check if two URI's are equal, only applies to absolute uri's
        /// </summary>
        public static bool IsEqual(this Uri uri, Uri otherUri)
        {
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
}