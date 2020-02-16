using System;
using System.IO;
using Windows.Storage;

namespace theRightDirection.Library.UnitTesting.UWP
{
    public abstract class UnitTestingBase
    {
        /// <summary>
        /// Gets the unit test bin\debug-directory.
        /// </summary>
        public string UnitTestDirectory
        {
            get
            {
                StorageFolder installedLocation = Windows.ApplicationModel.Package.Current.InstalledLocation;
                return installedLocation.Path;
            }
        }

        public string GetFilePathForFileFromUnitTestDirectory(string fileName, string folder = "")
        {
            return Path.Combine(UnitTestDirectory, folder, fileName);
        }
    }
}