using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace theRightDirection.CloudServices.Strava
{
    internal class StravaDataConverter
    {
        public IEnumerable<StravaActivity> ConvertToActivities(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new List<StravaActivity>();
            }
            var activities = JsonConvert.DeserializeObject<IEnumerable<StravaActivity>>(data);
            return from item in activities
                   let abc = SetHeartBeatData(item)
                   select item;
        }

        public StravaActivity ConvertToActivity(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return new StravaActivity();
            }
            var item = JsonConvert.DeserializeObject<StravaActivity>(data);
            SetHeartBeatData(item);
            return item;
        }

        private object SetHeartBeatData(StravaActivity item)
        {
            item.AverageHeartRate = item.AverageHeartRate < 40 ? 0 : item.AverageHeartRate;
            item.MaxHeartRate = item.MaxHeartRate < 40 ? 0 : item.MaxHeartRate;
            return -1;
        }

        public StravaUploadMessage ConvertToUploadMessage(string data)
        {
            return JsonConvert.DeserializeObject<StravaUploadMessage>(data);
        }
    }
}