using System;
using System.Collections.Generic;
using System.Text;

namespace theRightDirection.Library
{
    public interface IWebClient
    {
        bool UseDefaultCredentials { get; set; }
        string DownloadString(string url);
    }
}