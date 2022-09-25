using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using theRightDirection;

namespace theRightDirection
{
    public static class EnumHelper
    {
        /// <summary>
        /// Parses the text value to enum value which is given by type T
        /// </summary>
        /// <typeparam name="T">the type of the enumeration</typeparam>
        /// <param name="value">The value to parse</param>
        /// <param name="ignoreCase">Ignore casing</param>
        /// <returns>an enum value which belongs to T</returns>
        public static T ParseTextToEnumValue<T>(string value, bool ignoreCase = true)
        {
            try
            {
                var enumValue = Enum.Parse(typeof(T), value, ignoreCase);
                var result = (T)enumValue;
                return result;
            }
            catch (ArgumentException e)
            {
                throw new EnumHelperException(e.Message);
            }
        }

        /// <summary>
        /// Parses the text value to enum value which is given by type T in case of invalid value,
        /// the out parameter will always become the first value in the enumeration
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumValueAsString">The enum value as string</param>
        /// <param name="value">The value</param>
        /// <param name="ignoreCase">Ignore casing</param>
        /// <returns></returns>
        public static bool TryParseTextToEnumValue<T>(string enumValueAsString, out T value, bool ignoreCase = true)
        {
            try
            {
                value = ParseTextToEnumValue<T>(enumValueAsString, ignoreCase);
                return true;
            }
            catch
            {
                value = default(T);
                return false;
            }
        }

        public static IEnumerable<string> GetDescriptions<T>(bool descriptionsAsLowerCase = false)
        {
            List<string> result = new List<string>();
            var items = GetValues<T>();
            if (descriptionsAsLowerCase)
            {
                items.ForEach(x => result.Add(x.GetDescriptionAttribute().ToLowerInvariant()));
            }
            else
            {
                items.ForEach(x => result.Add(x.GetDescriptionAttribute()));
            }
            return result;
        }

        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToList();
        }

        public static T ParseIntToEnumValue<T>(int enumValue)
        {
            Array result = Enum.GetValues(typeof(T));
            object value = result.GetValue(enumValue);
            return (T)value;
        }

        public static T GetValueFromDescription<T>(string selectedWorkout)
        {
            Dictionary<string, T> values = new Dictionary<string, T>();
            var items = GetValues<T>();
            items.ForEach(x => values.Add(x.GetDescriptionAttribute(), x));
            return values[selectedWorkout];
        }
    }
}