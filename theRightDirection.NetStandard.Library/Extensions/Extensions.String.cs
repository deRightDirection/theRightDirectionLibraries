namespace theRightDirection
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Defines a collection of extensions for strings.
    /// </summary>
    public static partial class Extensions
    {
        public static bool IsValidFileName(this string filename, bool platformIndependent = false)
        {
            string sPattern = @"^(?!^(PRN|AUX|CLOCK\$|NUL|CON|COM\d|LPT\d|\..*)(\..+)?$)[^\x00-\x1f\\?*:\"";|/]+$";
            if (platformIndependent)
            {
                sPattern = @"^(([a-zA-Z]:|\\)\\)?(((\.)|(\.\.)|([^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?))\\)*[^\\/:\*\?""\|<>\. ](([^\\/:\*\?""\|<>\. ])|([^\\/:\*\?""\|<>]*[^\\/:\*\?""\|<>\. ]))?$";
            }
            return (Regex.IsMatch(filename, sPattern, RegexOptions.CultureInvariant));
        }

        public static string RemoveInvisibleCharacters(this string s)
        {
            return Regex.Replace(s, @"[^\u0009^\u000A^\u000D^\u0020-\u007E]", string.Empty);
        }

        public static Stream ToStream(this string s)
        {
            return s.ToStream(Encoding.UTF8);
        }

        public static Stream ToStream(this string s, Encoding encoding)
        {
            return new MemoryStream(encoding.GetBytes(s ?? ""));
        }

        /// <summary>
        /// Checks whether the specified text equals null or other value
        /// </summary>
        /// <param name="text">
        /// The text to check.
        /// </param>
        /// <param name="equalsTo">
        /// The string to compare with if the string is not null.
        /// </param>
        /// <returns>
        /// Returns true if the contained string is null or equals to the given value
        /// </returns>
        public static bool IsNullOrEqualsTo(this string text, string equalsTo)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }
            return text.Equals(equalsTo);
        }

        /// <summary>
        /// Checks whether the specified text contains another phrase.
        /// </summary>
        /// <param name="text">
        /// The text to check.
        /// </param>
        /// <param name="containedString">
        /// The string to check exists within the text.
        /// </param>
        /// <param name="compareOption">
        /// The compare option.
        /// </param>
        /// <returns>
        /// Returns true if the contained string exists in the text; else false.
        /// </returns>
        public static bool Contains(this string text, string containedString, CompareOptions compareOption)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, containedString, compareOption) >= 0;
        }

        /// <summary>
        /// Returns a copy of this <see cref="string"/> object converted to title case using the case rules of the invariant culture.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <example>
        /// In: HELLO WORLD
        /// Out: Hello World
        /// </example>
        /// <example>
        /// In: hOw ARE You?
        /// Out: How Are You?
        /// </example>
        public static string ToTitleCase(this string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            var result = new StringBuilder(value);
            result[0] = char.ToUpper(result[0]);
            for (var i = 1; i < result.Length; ++i)
            {
                result[i] = char.IsWhiteSpace(result[i - 1]) ? char.ToUpper(result[i]) : char.ToLower(result[i]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Returns a copy of this <see cref="string"/> object converted to default case using the case rules of the invariant culture.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <example>
        /// In: HELLO WORLD
        /// Out: Hello world
        /// </example>
        /// <example>
        /// In: hOw ARE You?
        /// Out: How are you?
        /// </example>
        public static string ToDefaultCase(this string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return string.Empty;

            var result = value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower();

            return result;
        }

        /// <summary>
        /// Converts a string to an array of bytes.
        /// </summary>
        /// <param name="str">
        /// The string to convert.
        /// </param>
        /// <returns>
        /// Returns the given string as an array of <see cref="byte"/>.
        /// </returns>
        public static byte[] ToByteArray(this string str)
        {
            return Encoding.UTF8.GetBytes(str);
        }

        public static bool IsAlphaNumeric(this string str, char[] extraAllowedCharacters = null)
        {
            return TestStringPattern(str, "^[a-zA-Z0-9XXX]+$", extraAllowedCharacters);
        }

        public static bool IsAlpha(this string str, char[] extraAllowedCharacters = null)
        {
            return TestStringPattern(str, "^[a-zA-ZXXX]+$", extraAllowedCharacters);
        }

        private static bool TestStringPattern(string test, string regex, char[] extraAllowedCharacters)
        {
            var testPattern = regex;
            if (extraAllowedCharacters == null)
            {
                testPattern = testPattern.Replace("XXX", string.Empty);
            }
            else
            {
                var extraPattern = new string(extraAllowedCharacters);
                testPattern = testPattern.Replace("XXX", extraPattern);
            }
            return Regex.Match(test, testPattern).Success;
        }

        // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
        // This size of the IV (in bytes) must = (keysize / 8).  Default keysize is 256, so the IV must be
        // 32 bytes long.  Using a 16 character string here gives us 32 bytes when converted to a byte array.
        private static readonly byte[] initVectorBytes = Encoding.ASCII.GetBytes("tu89geji340t89u2");

        // This constant is used to determine the keysize of the encryption algorithm.
        private const int keysize = 256;

        public static string Encrypt(this string plainText, string passPhrase)
        {
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                byte[] cipherTextBytes = memoryStream.ToArray();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(this string cipherText, string passPhrase)
        {
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
            using (PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null))
            {
                byte[] keyBytes = password.GetBytes(keysize / 8);
                using (RijndaelManaged symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.Mode = CipherMode.CBC;
                    using (ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes))
                    {
                        using (MemoryStream memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }
    }
}