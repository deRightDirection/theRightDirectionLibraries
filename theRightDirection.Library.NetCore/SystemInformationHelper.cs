using System;

namespace theRightDirection
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/windows/win32/cimwin32prov/win32-operatingsystem
    /// </summary>
    public class SystemInformationHelper
    {
        private OperatingSystem _windowsInformation;

        public SystemInformationHelper()
        {
            _windowsInformation = Environment.OSVersion;
        }

        public string WindowsVersionName => _windowsInformation.VersionString;

        public PlatformID Architecture => _windowsInformation.Platform;

        public int BuildNumber => _windowsInformation.Version.Build;

        public string ServicePackVersion => _windowsInformation.ServicePack;

        public string Version => $"{_windowsInformation.Version.Major}.{_windowsInformation.Version.Minor}.{_windowsInformation.Version.Build}";

    }
}