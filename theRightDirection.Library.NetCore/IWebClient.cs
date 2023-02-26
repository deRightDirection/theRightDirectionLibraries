using System;
using System.Collections.Specialized;

namespace theRightDirection
{
    public interface IWebClient : IDisposable
    {
        bool UseDefaultCredentials { get; set; }

        string DownloadString(string url);

        NameValueCollection QueryString { get; set; }

        byte[] UploadValues(string address, string method, NameValueCollection data);
    }
}