using System.Management;

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
            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            var information = searcher.Get();
            foreach (ManagementObject obj in information)
            {
                _osInformation = obj;
            }
        }

        public string WindowsVersionName => GetInformation("Caption");

        public string Architecture => GetInformation("OSArchitecture");

        public string BuildNumber => GetInformation("BuildNumber");

        public string ServicePackMajorVersion => GetInformation("ServicePackMajorVersion");

        public string ServicePackMinorVersion => GetInformation("ServicePackMinorVersion");

        public string Version => GetInformation("Version");

        private string GetInformation(string typeOfInformation) => _osInformation == null ? "Unknown" : _osInformation[typeOfInformation].ToString();
    }
}