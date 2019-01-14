using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.CloudServices.Strava
{
    internal class DoubleToIntJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof(double) || objectType == typeof(int));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            if (token.Type == JTokenType.Float)
            {
                var value = token.ToObject<double>();
                return (int)value;
            }
            if (token.Type == JTokenType.Integer)
            {
                return token.ToObject<int>();
            }
            if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
            {
                return null;
            }
            throw new JsonSerializationException("Unexpected token type: " +
                                                  token.Type.ToString());
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            double? d = default(double?);
            if (value != null)
            {
                d = new double?(Convert.ToDouble(value));
            }
            JToken.FromObject(d).WriteTo(writer);
        }
    }
}