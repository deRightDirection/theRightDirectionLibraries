using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace theRightDirection.CloudServices.Strava
{
    public class StravaService
    {
        private const string STRAVA_ACTIVITIES_URL = "https://www.strava.com/api/v3/activities";
        private const string STRAVA_UPLOADS_URI = "https://www.strava.com/api/v3/uploads";
        private const string STRAVA_ATHLETE_URL = "https://www.strava.com/api/v3/athlete";
        private const string STRAVA_GEAR_URL = "https://www.strava.com/api/v3/gear/";
        private string _accessToken;
        private StravaDataConverter _converter;
        private HttpClient _stravaHttpClient;

        public StravaService(string accessToken)
        {
            _stravaHttpClient = new HttpClient();
            //            _stravaHttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            _converter = new StravaDataConverter();
            _accessToken = accessToken;
        }

        public async Task<StravaActivity> GetActivityDataAsync(int activityID)
        {
            string uri = $"{STRAVA_ACTIVITIES_URL}/{activityID}?access_token={_accessToken}";
            //string uri = $"{STRAVA_ACTIVITIES_URL}/{activityID}";
            var data = await InteractWithStrava(_stravaHttpClient.GetAsync(uri));
            return _converter.ConvertToActivity(data);
        }

        [Obsolete("TODO: MME 17082018: deze functie doet eigenlijk nog niets", true)]
        public async Task<bool> GetAthleteInfoAsync()
        {
            string url = $"{STRAVA_ATHLETE_URL}?access_token={_accessToken}";
            var data = await InteractWithStrava(_stravaHttpClient.GetAsync(url));
            return true;
        }

        public async Task<StravaGear> GetGearAsync(string gearId)
        {
            string uri = $"{STRAVA_GEAR_URL}/{gearId}?access_token={_accessToken}";
            var data = await InteractWithStrava(_stravaHttpClient.GetAsync(uri));
            return JsonConvert.DeserializeObject<StravaGear>(data);
        }

        public async Task<IEnumerable<StravaActivity>> GetMyActivityDataAsync(DateTime synchronizeTillThisDate)
        {
            var activities = new List<StravaActivity>();
            int pageId = 1;
            bool continueRetrievingData = true;
            do
            {
                IEnumerable<StravaActivity> getActivitiesPerPage = null;
                try
                {
                    getActivitiesPerPage = await GetMyActivityDataAsync(pageId, synchronizeTillThisDate);
                    continueRetrievingData = getActivitiesPerPage.Count() == 200;
                    pageId++;
                    activities.AddRange(getActivitiesPerPage);
                }
                catch (StravaServiceException e)
                {
                    continueRetrievingData = false;
                }
            }
            while (continueRetrievingData);
            return activities;
        }

        public async Task<StravaActivity> UploadActivityAsync(StravaActivity activity)
        {
            // uploaden van activiteit zonder file
            if (string.IsNullOrEmpty(activity.FilePath))
            {
                string postUrl = $"{STRAVA_ACTIVITIES_URL}?access_token={_accessToken}";
                var postMessage = await SendPostAsync(postUrl, activity);
                return _converter.ConvertToActivity(postMessage);
            }
            // uploaden van activiteit met file
            if (!string.IsNullOrEmpty(activity.FilePath) && activity.FileType != StravaFileUploadFileType.Unknown)
            {
                string postUrl = $"{STRAVA_UPLOADS_URI}";
                var postMessage = await UploadAsync(postUrl, activity);
                var uploadMessage = _converter.ConvertToUploadMessage(postMessage);
                while (uploadMessage.ActivityId == null)
                {
                    if (!string.IsNullOrEmpty(uploadMessage.Error))
                    {
                        throw new StravaServiceException(uploadMessage.Error);
                    }
                    await Task.Delay(1000);
                    uploadMessage = await GetUploadStatus(uploadMessage.Id);
                }
                return await GetActivityDataAsync(uploadMessage.ActivityId.Value);
            }
            return new StravaActivity();
        }

        private async Task<StravaUploadMessage> GetUploadStatus(int activityId)
        {
            string uri = $"{STRAVA_UPLOADS_URI}/{activityId}?access_token";
            var data = await InteractWithStrava(_stravaHttpClient.GetAsync(uri));
            return _converter.ConvertToUploadMessage(data);
        }

        public async Task<StravaActivity> UpdateActivityAsync(StravaActivity activity)
        {
            var putUrl = $"{STRAVA_ACTIVITIES_URL}/{activity.Id}?access_token={_accessToken}";
            var putMessage = await SendPutAsync(putUrl, activity);
            return _converter.ConvertToActivity(putMessage);
        }

        [Obsolete("niet gebruiken nog, ongeteste code (15082018 MME)", true)]
        public async Task<string> DeleteActivityAsync(int activityID)
        {
            string uri = $"{STRAVA_ACTIVITIES_URL}/{activityID}?access_token={_accessToken}";
            return await InteractWithStrava(_stravaHttpClient.DeleteAsync(uri));
        }

        private async Task<IEnumerable<StravaActivity>> GetMyActivityDataAsync(int page, DateTime synchronizeTillThisDate)
        {
            var unixTimeStamp = synchronizeTillThisDate.AddDays(-1).TimeStampInSeconds();
            string uri = $"{STRAVA_ACTIVITIES_URL}?per_page=200&page={page}&access_token={_accessToken}&after={unixTimeStamp}";
            var data = await InteractWithStrava(_stravaHttpClient.GetAsync(uri));
            return _converter.ConvertToActivities(data);
        }

        private async Task<string> SendPutAsync(string uri, StravaActivity activity)
        {
            var putData = new List<KeyValuePair<string, string>>();
            putData.Add(new KeyValuePair<string, string>("name", activity.Title));
            putData.Add(new KeyValuePair<string, string>("elapsed_time", activity.Duration.ToString()));
            putData.Add(new KeyValuePair<string, string>("start_date_local", activity.Date.ToString("dd/MM/yyyy HH:mm:ss")));
            putData.Add(new KeyValuePair<string, string>("type", activity.Type.ToString()));
            putData.Add(new KeyValuePair<string, string>("description", activity.Comment));
            putData.Add(new KeyValuePair<string, string>("distance", activity.Distance.ToString()));
            putData.Add(new KeyValuePair<string, string>("gear_id", activity.GearId.ToString()));
            // TODO: isRace toevoegen, kan nu nog niet via API (15082018)
            HttpContent content = new FormUrlEncodedContent(putData);
            return await InteractWithStrava(_stravaHttpClient.PutAsync(uri, content));
        }

        private async Task<string> SendPostAsync(string uri, StravaActivity activity)
        {
            var postData = new List<KeyValuePair<string, string>>();
            postData.Add(new KeyValuePair<string, string>("name", activity.Title));
            postData.Add(new KeyValuePair<string, string>("elapsed_time", activity.Duration.ToString()));
            postData.Add(new KeyValuePair<string, string>("start_date_local", activity.Date.ToString("dd/MM/yyyy HH:mm:ss")));
            postData.Add(new KeyValuePair<string, string>("type", activity.Type.ToString()));
            postData.Add(new KeyValuePair<string, string>("description", activity.Comment));
            postData.Add(new KeyValuePair<string, string>("distance", activity.Distance.ToString()));
            var content = new FormUrlEncodedContent(postData);
            //    content2.Add(content);
            // TODO: isRace toevoegen, kan nu nog niet via API (15082018)
            return await InteractWithStrava(_stravaHttpClient.PostAsync(uri, content));
        }

        private async Task<string> UploadAsync(string uri, StravaActivity activity)
        {
            _stravaHttpClient.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", _accessToken));
            MultipartFormDataContent content = new MultipartFormDataContent();
            byte[] bytes = await Task.Run(() => File.ReadAllBytes(activity.FilePath));
            var streamContent = new ByteArrayContent(bytes);
            content.Add(streamContent, "file", activity.FilePath);
            var completeUri = $"{uri}?data_type={activity.FileType}&activity_type={activity.Type.ToString()}&name={activity.Title}&description={activity.Comment}";
            return await InteractWithStrava(_stravaHttpClient.PostAsync(completeUri, content));
        }

        private async Task<string> InteractWithStrava(Task<HttpResponseMessage> function)
        {
            using (HttpResponseMessage response = await function)
            {
                if (response != null)
                {
                    var responseMessage = await response.Content.ReadAsStringAsync();
                    var isError = false;
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.Created:
                            return responseMessage;
                        case HttpStatusCode.OK:
                            return responseMessage;
                        case HttpStatusCode.NotFound:
                            return string.Empty;
                        default:
                            isError = true;
                            break;
                    }
                    if (isError)
                    {
                        throw new StravaServiceException(responseMessage);
                    }
                }
            }
            return string.Empty;
        }
    }
}