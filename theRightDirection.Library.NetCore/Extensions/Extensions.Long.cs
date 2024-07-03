using System;

namespace theRightDirection;

public static partial class Extensions
{
    /// <summary>
    /// transform a long value to a file size
    /// </summary>
    /// <param name="fileLength"></param>
    /// <returns></returns>
    public static string ToFileLengthRepresentation(this long fileLength, bool spaceBetweenNumberAndAbbreviation = true)
    {
        long KB = 1024;
        long MB = KB * 1024;
        long GB = MB * 1024;
        long TB = GB * 1024;
        double size = fileLength;
        if (fileLength >= TB)
        {
            size = Math.Round((double)fileLength / TB, 2);
            return $"{size} Tb";
        }

        if (fileLength >= GB)
        {
            size = Math.Round((double)fileLength / GB, 2);
            return $"{size} Gb";
        }

        if (fileLength >= MB)
        {
            size = Math.Round((double)fileLength / MB, 2);
            return $"{size} Mb";
        }

        if (fileLength >= KB)
        {
            size = Math.Round((double)fileLength / KB, 2);
            return $"{size} kb";
        }

        return $"{size} Bytes";
    }
}