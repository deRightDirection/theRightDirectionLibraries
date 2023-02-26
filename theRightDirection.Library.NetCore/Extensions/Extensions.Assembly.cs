using System.IO;
using System.Reflection;

namespace theRightDirection
{
    public static partial class Extensions
    {
        /// <summary>
        /// Finds a file next to assembly, for example the configuration file
        /// if the exception is suppressed the method returns string.empty
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="suppressFileNotFoundException">if set to <c>true</c> [suppress file not found exception].</param>
        /// <returns>
        /// path to the file
        /// </returns>
        /// <exception cref="System.IO.FileNotFoundException"></exception>
        /// <exception cref="FileNotFoundException">In case of the file is not found</exception>
        public static string FindFileNextToAssembly(this Assembly assembly, string fileName, bool suppressFileNotFoundException = false)
        {
            string path = Path.Combine(assembly.DirectoryOfAssembly(), fileName);
            if (!File.Exists(path))
            {
                var message = string.Format("File is not found next to assembly: {0}", path);
                if (!suppressFileNotFoundException)
                {
                    throw new FileNotFoundException(message);
                }
                else
                {
                    path = string.Empty;
                }
            }
            return path;
        }

        /// <summary>
        /// Finds the directory of the assembly
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>directory which the assembly contains</returns>
        public static string DirectoryOfAssembly(this Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.Location);
        }
    }
}