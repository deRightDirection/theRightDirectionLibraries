using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;

namespace theRightDirection
{
    public static partial class Extensions
    {
        /// <summary>
        /// convert a string into secure string so the value of the string can't be read
        /// in the memory while the program is running
        /// </summary>
        public static SecureString ToSecureString(this string unsecureString)
        {
            if (unsecureString == null) throw new ArgumentNullException("unsecureString");
            return unsecureString.Aggregate(new SecureString(), AppendChar, MakeReadOnly);
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