using System;
using System.Collections.Generic;
using System.Text;

namespace theRightDirection.Library.CloudServices
{
    public struct CloudServiceConnectResult
    {
        public CloudServiceConnectResult(bool isConnected, string errorMessage = "")
        {
            ErrorMessage = errorMessage;
            IsConnected = isConnected;
        }
        public string ErrorMessage { get; private set; }
        public bool IsConnected { get; private set; }
    }
}
