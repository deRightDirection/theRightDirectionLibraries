using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace theRightDirection.Library
{
    public static partial class Extensions
    {
        /// <summary>
        /// check of a string can be used as a valid filename
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="platformIndependent"></param>
        /// <returns></returns>
        public static bool IsValidFileName(this string filename, bool platformIndependent = false)
        {
            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }
            return (Regex.IsMatch(filename, sPattern, RegexOptions.CultureInvariant));
        }

        public static string ToUnsecureString(this SecureString secureString)
        {
            if (secureString == null) throw new ArgumentNullException("secureString");

            var unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(secureString);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

        public static SecureString ToSecureString(this string unsecureString)
        {
            if (unsecureString == null) throw new ArgumentNullException("unsecureString");

            return unsecureString.Aggregate(new SecureString(), AppendChar, MakeReadOnly);
        }

        /// <summary>
        /// convert a string to a stream
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static Stream ToStream(this string s)
        {
            return s.ToStream(Encoding.UTF8);
        }

        /// <summary>
        ///  convert a string to a stream and define the encoding
        /// </summary>
        /// <param name="s"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static Stream ToStream(this string s, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(s ?? ""));
        }

        /// <summary>
        /// Returns characters slices from string between two indexes.
        /// 
        /// If start or end are negative, their indexes will be calculated counting 
        /// back from the end of the source string. 
        /// If the end param is less than the start param, the Slice will return a 
        /// substring in reverse order.
        /// 
        /// <param name="source">String the extension method will operate upon.</param>
        /// <param name="startIndex">Starting index, may be negative.</param>
        /// <param name="endIndex">Ending index, may be negative).</param>
        /// </summary>
        public static string Slice(this string source, int startIndex, int endIndex = int.MaxValue)
        {
            // If startIndex or endIndex exceeds the length of the string they will be set 
            // to zero if negative, or source.Length if positive.
            if (source.ExceedsLength(startIndex)) startIndex = startIndex < 0 ? 0 : source.Length;
            if (source.ExceedsLength(endIndex)) endIndex = endIndex < 0 ? 0 : source.Length;
            // Negative values count back from the end of the source string.
            if (startIndex < 0) startIndex = source.Length + startIndex;
            if (endIndex < 0) endIndex = source.Length + endIndex;
            // Calculate length of characters to slice from string.
            int length = Math.Abs(endIndex - startIndex);
            // If the endIndex is less than the startIndex, return a reversed substring.
            if (endIndex < startIndex) return source.Substring(endIndex, length).Reverse();
            return source.Substring(startIndex, length);
        }

        /// <summary>
        /// Reverses character order in a string.
        /// </summary>
        /// <param name="source"></param>
        /// <returns>string</returns>
        public static string Reverse(this string source)
        {
            char[] charArray = source.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// Verifies that the index is within the range of the string source.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns>bool</returns>
        public static bool ExceedsLength(this string source, int index)
        {
            return Math.Abs(index) > source.Length ? true : false;
        }

        private static SecureString MakeReadOnly(SecureString ss)
        {
            ss.MakeReadOnly();
            return ss;
        }

        private static SecureString AppendChar(SecureString ss, char c)
        {
            ss.AppendChar(c);
            return ss;
        }
    }
}