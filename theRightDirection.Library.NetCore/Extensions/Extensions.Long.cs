using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection
{
    public static partial class Extensions
    {
        /// <summary>
        /// transform a long value to a file size
        /// </summary>
        /// <param name="fileLength"></param>
        /// <returns></returns>
        public static string ToFileLengthRepresentation(this long fileLength)
        {
            if (fileLength >= 1 << 30)
                return $"{fileLength >> 30}Gb";

            if (fileLength >= 1 << 20)
                return $"{fileLength >> 20}Mb";

            if (fileLength >= 1 << 10)
                return $"{fileLength >> 10}Kb";

            return $"{fileLength}B";
        }
    }
}