using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection
{
    public class UriHelper
    {
        /// <summary>
        /// create an uri from address and port
        /// </summary>
        public static Uri CreateFromAddressAndPort(string address, int port)
        {
            try
            {
                var uriBuilder = new UriBuilder(address) { Port = port };
                return uriBuilder.Uri;
            }
            catch
            {
                return null;
            }
        }
    }
}