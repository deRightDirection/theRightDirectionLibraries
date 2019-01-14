using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using theRightDirection;

namespace theRightDirection.CloudServices.Strava
{
    internal class StravaSportsTypeConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var enumValue = (StravaSportsType)value;
            writer.WriteValue(enumValue.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var enumString = (string)reader.Value;
            StravaSportsType sportsType;
            EnumHelper.TryParseTextToEnumValue(enumString, out sportsType);
            return sportsType;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}