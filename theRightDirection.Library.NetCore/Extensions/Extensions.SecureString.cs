using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace theRightDirection
{
    /// <summary>
    /// Extension for use with SecureString and String objects for simplying secure conversion
    /// of Strings to a secure form and back
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Original String value is automatically zeroed out leaving only the secure encrypted object in one step.
        /// Call: SecureString s = myString.toSecureString();
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static SecureString ToSecureString(this string value, bool leaveOriginal = false, bool makeReadOnly = true)
        {
            if(value.IsNullOrEmpty())
            {
                return new SecureString();
            }
            value.CheckNullRef();
            SecureString secureString;

            unsafe
            {
                fixed (char* chars = value)
                {
                    //create encrypted secure string object
                    secureString = new SecureString(chars, value.Length);
                    if (makeReadOnly)
                        secureString.MakeReadOnly(); //.AddChar and InsertAt methods won't work if true

                    if (!leaveOriginal)
                        value.SecureClear();
                }
            }

            return secureString;
        }

        /// <summary>
        /// Decrypts the SecureString converting it to a String for convenience and
        /// systematic method of doing on the object
        /// Call: String s = secureObject.ConvertToString();
        /// </summary>
        /// <param name="SecureString">An allocated SecureString object</param>
        /// <returns>The decrypted String</returns>
        public static string ToUnsecureString(this SecureString value)
        {
            if(value == null || value.Length == 0)
            {
                return null;
            }
            value.CheckNullRef();
            IntPtr stringPointer = IntPtr.Zero;
            var result = string.Empty;
            try
            {
                stringPointer = Marshal.SecureStringToGlobalAllocUnicode(value);
                result = Marshal.PtrToStringUni(stringPointer);
            }
            finally
            {
                if (stringPointer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeGlobalAllocUnicode(stringPointer);
                }
            }

            return result;
        }

        /// <summary>
        /// Facilitates comparison of two SecureString objects, decrypting each briefly then zeroing
        /// out the cleartext once the comparison is performed.
        /// Call: SecureString.SecureCompare(SecureString object);
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool SecureCompare(this SecureString left, SecureString right)
        {
            bool result;

            //temporarily decrypt both SecureString objects
            string leftValue = ToUnsecureString(left);
            string rightValue = ToUnsecureString(right);

            result = leftValue == rightValue;

            //clear memory
            leftValue.SecureClear();
            rightValue.SecureClear();

            return result;
        }

        /// <summary>
        /// Offers existing String type a safe zeroing out method without returning a SecureString object while taking care
        /// not to zero out string literals or values that share the same location as a string literal
        /// Call: myString.SecureClear();
        /// </summary>
        /// <param name="value"></param>
        public static void SecureClear(this string value)
        {
            object checkInterned = string.IsInterned(value);
            if (checkInterned == null)
            {
                unsafe
                {
                    fixed (char* chars = value)
                    {
                        //zero out original
                        for (int i = 0; i < value.Length; i++)
                            chars[i] = '\0';
                    }
                }
            }
        }

        private static void CheckNullRef(this object value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }
        }
    }
}