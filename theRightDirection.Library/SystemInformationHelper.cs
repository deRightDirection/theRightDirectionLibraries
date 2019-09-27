using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.Library
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-operatingsystem
    /// </summary>
    public class SystemInformationHelper
    {
        private ManagementObject _osInformation;
        public SystemInformationHelper()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                ManagementObjectCollection information = searcher.Get();
                if (information != null)
                {
                    foreach (ManagementObject obj in information)
                    {
                        _osInformation = obj;
                    }
                }

            }
        }

        public string WindowsVersionName
        {
            get
            {
                return GetInformation("Caption");
            }
        }

        public string Architecture
        {
            get
            {
                return GetInformation("OSArchitecture");
            }
        }

        public string BuildNumber
        {
            get
            {
                return GetInformation("BuildNumber");
            }
        }

        public string ServicePackMajorVersion
        {
            get
            {
                return GetInformation("ServicePackMajorVersion");
            }
        }
        public string ServicePackMinorVersion
        {
            get
            {
                return GetInformation("ServicePackMinorVersion");
            }
        }
        public string Version
        {
            get
            {
                return GetInformation("Version");
            }
        }
        private string GetInformation(string typeOfInformation)
        {
            if (_osInformation == null)
            {
                return "Unknown";
            }
            return _osInformation[typeOfInformation].ToString();
        }

    }
}