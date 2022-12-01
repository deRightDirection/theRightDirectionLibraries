using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace theRightDirection
{
    public static partial class Extensions
    {
        public static string ToBase64(this string text)
        {
            return ToBase64(text, Encoding.UTF8);
        }

        public static string ToBase64(this string text, Encoding encoding)
        {
            if (string.IsNullOrEmpty(text))
            {
                return text;
            }

            byte[] textAsBytes = encoding.GetBytes(text);
            return Convert.ToBase64String(textAsBytes);
        }

        public static bool TryParseBase64(this string text, out string decodedText)
        {
            return TryParseBase64(text, Encoding.UTF8, out decodedText);
        }

        public static bool TryParseBase64(this string text, Encoding encoding, out string decodedText)
        {
            if (string.IsNullOrEmpty(text))
            {
                decodedText = text;
                return false;
            }

            try
            {
                byte[] textAsBytes = Convert.FromBase64String(text);
                decodedText = encoding.GetString(textAsBytes);
                return true;
            }
            catch (Exception)
            {
                decodedText = null;
                return false;
            }
        }

        /// <summary>
        /// split a string at the captital letters and at a space
        /// </summary>
        public static string SplitOnCapitalLetters(this string inputString)
        {
            if (string.IsNullOrEmpty(inputString))
            {
                return string.Empty;
            }
            var result = new StringBuilder();
            foreach (var ch in inputString)
            {
                if (char.IsUpper(ch) && result.Length > 0)
                {
                    result.Append(' ');
                }
                result.Append(ch);
            }
            return result.ToString();
        }

        /// <summary>
        /// remove character at the end of a string which is not a letter or digit
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string RemoveSpecialCharacterAtTheEndFromString(this string input)
        {
            StringBuilder sb = new StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
            {
                if (i < input.Length - 1)
                {
                    sb.Append(input[i]);
                }
                else if (char.IsLetterOrDigit(input[i]))
                {
                    sb.Append(input[i]);
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// check if the string can be a valid e-mailaddress
        /// </summary>
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }
            string sPattern = @"^((""[\w -\\s] + "")|([\w-]+(?:\.[\w-]+)*)|(""[\w -\\s] + "")([\w-]+(?:\.[\w-]+)*))(@((?:[\w-]+\.)*\w[\w-]{0,66})\.([a-z]{2,6}(?:\.[a-z]{2})?)$)|(@\[?((25[0-5]\.|2[0-4][0-9]\.|1[0-9]{2}\.|[0-9]{1,2}\.))((25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\.){2}(25[0-5]|2[0-4][0-9]|1[0-9]{2}|[0-9]{1,2})\]?$)";
            return (Regex.IsMatch(email, sPattern, RegexOptions.CultureInvariant));
        }

        /// <summary>
        /// convert the passphrase to a 32 byte string
        /// </summary>
        public static byte[] ConvertToKey(this string passPhrase)
        {
            var length = 32;
            var bytes = new byte[length];
            int len = passPhrase.Length > length ? length : passPhrase.Length;
            Encoding.UTF8.GetBytes(passPhrase.Substring(0, len)).CopyTo(bytes, 0);
            return bytes;
        }

        /// <summary>
        /// encrypt a text
        /// </summary>
        public static string Encrypt(this string plainText, string passPhrase)
        {
            if (string.IsNullOrEmpty(passPhrase))
                throw new ArgumentException("Key must have valid value.", nameof(passPhrase));
            if (string.IsNullOrEmpty(plainText))
                throw new ArgumentException("The text must have valid value.", nameof(plainText));

            var buffer = Encoding.UTF8.GetBytes(plainText);
            var hash = SHA512.Create();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(passPhrase)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                    throw new ArgumentException("Parameter must not be null.", nameof(aes));

                aes.Key = aesKey;

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, encryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(buffer))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    var result = resultStream.ToArray();
                    var combined = new byte[aes.IV.Length + result.Length];
                    Array.ConstrainedCopy(aes.IV, 0, combined, 0, aes.IV.Length);
                    Array.ConstrainedCopy(result, 0, combined, aes.IV.Length, result.Length);

                    return Convert.ToBase64String(combined);
                }
            }
        }

        public static string Decrypt(this string cipherText, string passPhrase)
        {
            if (string.IsNullOrEmpty(passPhrase))
                throw new ArgumentException("Key must have valid value.", nameof(passPhrase));
            if (string.IsNullOrEmpty(cipherText))
                throw new ArgumentException("The encrypted text must have valid value.", nameof(cipherText));
            byte[] combined;
            try
            {
                combined = Convert.FromBase64String(cipherText);
            }
            catch
            {
                throw new ArgumentException("The encrypted text is not encrypted properly.", nameof(cipherText));
            }
            var buffer = new byte[combined.Length];
            var hash = SHA512.Create();
            var aesKey = new byte[24];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(passPhrase)), 0, aesKey, 0, 24);

            using (var aes = Aes.Create())
            {
                if (aes == null)
                    throw new ArgumentException("Parameter must not be null.", nameof(aes));

                aes.Key = aesKey;

                var iv = new byte[aes.IV.Length];
                var ciphertext = new byte[buffer.Length - iv.Length];

                Array.ConstrainedCopy(combined, 0, iv, 0, iv.Length);
                Array.ConstrainedCopy(combined, iv.Length, ciphertext, 0, ciphertext.Length);

                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                using (var resultStream = new MemoryStream())
                {
                    using (var aesStream = new CryptoStream(resultStream, decryptor, CryptoStreamMode.Write))
                    using (var plainStream = new MemoryStream(ciphertext))
                    {
                        plainStream.CopyTo(aesStream);
                    }

                    return Encoding.UTF8.GetString(resultStream.ToArray());
                }
            }
        }

        /// <summary>
        /// checks if the data is Json, it use the dependency Newtonsoft.Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool IsValidJson<T>(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            data = data.Trim();
            var objectJson = data.StartsWith("{") && data.EndsWith("}");
            var arrayJson = data.StartsWith("[") && data.EndsWith("]");
            if (objectJson || arrayJson)
            {
                try
                {
                    JsonConvert.DeserializeObject<T>(data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// checks if the data is Json, it use the dependency Newtonsoft.Json
        /// </summary>
        public static bool IsValidJson(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }
            data = data.Trim();
            var objectJson = data.StartsWith("{") && data.EndsWith("}");
            var arrayJson = data.StartsWith("[") && data.EndsWith("]");
            if (objectJson || arrayJson)
            {
                try
                {
                    JObject.Parse(data);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks whether the specified text contains another phrase.
        /// </summary>
        /// <param name="text">The text to check.</param>
        /// <param name="containedString">The string to check exists within the text.</param>
        /// <param name="compareOption">The compare option.</param>
        /// <returns>Returns true if the contained string exists in the text; else false.</returns>
        public static bool Contains(this string text, string containedString, CompareOptions compareOption)
        {
            return CultureInfo.CurrentCulture.CompareInfo.IndexOf(text, containedString, compareOption) >= 0;
        }

        public static bool ContainsWithRegex(this string text, string regex)
        {
            var searchText = text.ToLowerInvariant();
            return TestStringPattern(searchText, regex);
        }

        public static string RemoveSpecialCharactersFromString(this string text, string replacement = "", char[] extraAllowedCharacters = null)
        {
            if (string.IsNullOrEmpty(text))
            {
                return "";
            }
            var stringBuilder = new StringBuilder();
            foreach (var letter in text)
            {
                if (letter.ToString().IsAlphaNumeric(extraAllowedCharacters))
                {
                    stringBuilder.Append(letter);
                }
                else
                {
                    if (!string.IsNullOrEmpty(replacement))
                    {
                        stringBuilder.Append(replacement);
                    }
                }
            }
            return stringBuilder.ToString();
        }

        private static bool TestStringPattern(string test, string regex, char[] extraAllowedCharacters = null)
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

        /// <summary>
        /// truncate a string to specific length
        /// </summary>
        /// <param name="value"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Truncate(this string value, int length)
        {
            if (value.Length > length) return value.Substring(0, length);
            return value;
        }

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

        /// <summary>
        /// <param name="throwExceptionWhenNull">throw argumentnull-exception when unsecurestring is null</param>
        /// <param name="convertNullToEmptyString">in case you want to be sure that null-values not breaking your code, they can be switch to string empty</param>
        /// </summary>
        [Obsolete("27-04-2022: new implementation available, this one renamed to ToUnSecureString_Obsolete",true)]
        public static string ToUnsecureString_Obsolete(this SecureString secureString, bool throwExceptionWhenNull = true, bool convertNullToEmptyString = false)
        {
            if (secureString == null && throwExceptionWhenNull)
            {
                throw new ArgumentNullException("secureString");
            }
            if (convertNullToEmptyString && secureString == null)
            {
                return null;
            }
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
        /// convert a string to a stream and define the encoding
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
        /// If start or end are negative, their indexes will be calculated counting back from the
        /// end of the source string. If the end param is less than the start param, the Slice will
        /// return a substring in reverse order.
        ///
        /// <param name="source">String the extension method will operate upon.</param><param
        /// name="startIndex">Starting index, may be negative.</param><param name="endIndex">Ending
        /// index, may be negative).</param>
        /// </summary>
        public static string Slice(this string source, int startIndex, int endIndex = int.MaxValue)
        {
            // If startIndex or endIndex exceeds the length of the string they will be set to zero
            // if negative, or source.Length if positive.
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

        /// <summary>
        /// convert a string into secure string so the value of the string can't be read in the
        /// memory while the program is running
        /// <param name="throwExceptionWhenNull">throw argumentnull-exception when unsecurestring is null</param>
        /// <param name="convertNullToEmptyString">in case you want to be sure that null-values not breaking your code, they can be switch to string empty</param>
        /// </summary>
        [Obsolete("27-04-2022: new implementation available, this one renamed to ToSecureString_Obsolete", true)]
        public static SecureString ToSecureString_Obsolete(this string unsecureString, bool throwExceptionWhenNull = true, bool convertNullToEmptyString = false)
        {
            if (unsecureString == null && throwExceptionWhenNull)
            {
                throw new ArgumentNullException("unsecureString");
            }

            if (unsecureString == null && convertNullToEmptyString)
            {
                unsecureString = string.Empty;
            }
            return unsecureString.Aggregate(new SecureString(), AppendChar, MakeReadOnly);
        }

        public static bool IsNumeric(this string str)
        {
            return TestStringPattern(str, "^[0-9]+$");
        }

        public static bool IsAlphaNumeric(this string str, char[] extraAllowedCharacters = null)
        {
            return TestStringPattern(str, "^[a-zA-Z0-9XXX]+$", extraAllowedCharacters);
        }

        public static bool IsAlpha(this string str, char[] extraAllowedCharacters = null)
        {
            return TestStringPattern(str, "^[a-zA-ZXXX]+$", extraAllowedCharacters);
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