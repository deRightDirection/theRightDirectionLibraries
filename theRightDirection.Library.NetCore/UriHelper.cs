using System;

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