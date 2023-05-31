using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace theRightDirection.Json
{
    /// <summary>
    /// convert a json-string to the string-value of an enum, so if not the numeric value is in the json it converts the value based on the string
    /// EnumHelper, also from this library, is internally used for this conversion
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class StringEnumConverter<T> : StringEnumConverter where T : Enum
    {
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumValueString = reader.Value?.ToString();
            if (enumValueString == null)
            {
                var log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
                log.Warn($"{typeof(T)} value is null or empty");
                return default(T);
            }
            enumValueString = enumValueString.Replace(" ", string.Empty);
            enumValueString = enumValueString.Replace("-", string.Empty);
            var parsingSucceeeded = EnumHelper.TryParseTextToEnumValue(enumValueString, out T enumValue);
            if (parsingSucceeeded)
            {
                return enumValue;
            }
            var result = default(T);
            try
            {
                return base.ReadJson(reader, objectType, existingValue, serializer);
            }
            catch
            {
                // ignored
            }
            return result;
        }
    }
}
