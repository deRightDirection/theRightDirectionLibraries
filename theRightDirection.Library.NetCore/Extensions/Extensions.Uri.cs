﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection
{
    public static partial class Extensions
    {
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