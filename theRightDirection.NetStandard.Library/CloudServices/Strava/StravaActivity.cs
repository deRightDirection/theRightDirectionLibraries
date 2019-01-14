using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Globalization;
using theRightDirection.Services.Sports;

namespace theRightDirection.CloudServices.Strava
{
    public class StravaActivity
    {
        public StravaActivity()
        {
        }

        [JsonProperty("average_heartrate")]
        public double AverageHeartRate { get; set; }

        [JsonProperty("average_speed")]
        public double AverageSpeed { get; set; }

        [JsonConverter(typeof(DoubleToIntJsonConverter))]
        [JsonProperty("calories")]
        public int Calories { get; set; }

        [JsonProperty("description")]
        public string Comment { get; set; }

        [JsonProperty("start_date_local")]
        public DateTime Date { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }

        [JsonProperty("moving_time")]
        public int Duration { get; set; }

        [JsonProperty("gear_id")]
        public string GearId { get; set; }

        /// <summary>
        /// Strava activity ID
        /// </summary>
        [JsonProperty("id")]
        public int Id { get; set; }

        public bool IsRace { get; set; }

        [JsonProperty("max_heartrate")]
        public double MaxHeartRate { get; set; }

        [JsonProperty("max_speed")]
        public double MaxSpeed { get; set; }

        [JsonConverter(typeof(StravaSportsTypeConverter))]
        [JsonProperty("type")]
        public StravaSportsType Type { get; set; }

        [JsonProperty("workout_type")]
        [DefaultValue(0)]
        public int? WorkoutType { get; set; }

        [JsonProperty("name")]
        public string Title { get; set; }

        [JsonProperty("trainer")]
        public bool Trainer { get; set; }

        [JsonIgnore]
        public StravaFileUploadFileType FileType { get; set; }

        [JsonIgnore]
        public string FilePath { get; set; }
    }
}