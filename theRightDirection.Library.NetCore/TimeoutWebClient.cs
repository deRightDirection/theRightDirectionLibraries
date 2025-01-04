using System;
using System.Net;

namespace theRightDirection;

/// <summary>
/// webclient with configurable timeout-period
/// </summary>
[Obsolete("dependencies are obsolete so do not use this class anymore, will be removed in future releases")]
public class TimeoutWebClient : WebClient, IWebClient
{
    /// <summary>
    /// time out in milliseconds
    /// </summary>
    [Obsolete("dependencies are obsolete so do not use this class anymore, will be removed in future releases")]
    public int Timeout { get; set; }

    [Obsolete("dependencies are obsolete so do not use this class anymore, will be removed in future releases")]
    public TimeoutWebClient()
    {
        Timeout = 60000;
    }

    [Obsolete("dependencies are obsolete so do not use this class anymore, will be removed in future releases")]
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