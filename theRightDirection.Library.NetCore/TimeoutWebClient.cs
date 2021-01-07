using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace theRightDirection
{
    /// <summary>
    /// webclient with configurable timeout-period
    /// </summary>
    public class TimeoutWebClient : WebClient, IWebClient
    {
        /// <summary>
        /// time out in milliseconds
        /// </summary>
        public int Timeout { get; set; }

        public TimeoutWebClient()
        {
            Timeout = 60000;
        }

        public TimeoutWebClient(int timeout)
        {
            Timeout = timeout;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            var request = base.GetWebRequest(address);
            request.Timeout = Timeout;
            return request;
        }
    }
}