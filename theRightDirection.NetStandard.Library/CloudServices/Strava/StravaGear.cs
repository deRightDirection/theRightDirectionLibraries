using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace theRightDirection.CloudServices.Strava
{
    public class StravaGear
    {
        [JsonProperty("brand_name")]
        public string Brand { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("frame_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public StravaBikeFrameType FrameType { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("model_name")]
        public string Model { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}