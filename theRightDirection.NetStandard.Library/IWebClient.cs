using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace theRightDirection.Library
{
    public interface IWebClient : IDisposable
    {
        bool UseDefaultCredentials { get; set; }
        string DownloadString(string url);

        NameValueCollection QueryString { get; set; }
        byte[] UploadValues(string address, string method, NameValueCollection data);
    }
}