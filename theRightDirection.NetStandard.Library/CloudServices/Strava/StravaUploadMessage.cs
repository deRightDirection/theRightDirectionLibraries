using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace theRightDirection.CloudServices.Strava
{
    public class StravaUploadMessage
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("activity_id")]
        [DefaultValue(0)]
        public int? ActivityId { get; set; }
        [JsonProperty("error")]
        public string Error { get;set; }
    }
}
