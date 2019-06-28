using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library
{
    public static partial class Extensions
    {        
        public static Uri RewriteUriToSecureHttps(Uri uri)
        {
            return new UriBuilder(uri)
            {
                Scheme = Uri.UriSchemeHttps,
                Port = uri.IsDefaultPort ? -1 : uri.Port
            }.Uri;
        }
    }
}