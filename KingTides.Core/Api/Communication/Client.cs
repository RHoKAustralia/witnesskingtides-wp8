using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;

namespace KingTides.Core.Api.Communication
{
    public class Client
    {
        private readonly Uri _endpoint;
        private readonly IWebRequestFactory _webRequestFactory;

        public Client(string endpoint, IWebRequestFactory webRequestFactory)
        {
            _endpoint = new Uri(endpoint);
            _webRequestFactory = webRequestFactory;
        }

        public async Task<KingTideEvent[]> GetKingTideEventsAsync()
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint, "/tide_events"));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<KingTideEvent[]>(response);
        }

        public async Task<KingTideEvent[]> GetFutureKingTideEventsAsync(DateTime? future = null)
        {
            var path = string.Format("/tide_events/future/{0}", future.HasValue ? future.Value.ToString("o") : string.Empty);
            var client = _webRequestFactory.Create(new Uri(_endpoint, path));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<KingTideEvent[]>(response);
        }

        public async Task<KingTideEvent[]> GetCurrentKingTideEventsAsync()
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint, "/tide_events/current"));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<KingTideEvent[]>(response);
        }

        public async Task<Photo[]> GetPhotosForEmailAsync(string email)
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint, "/photos?email=" + email));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<Photo[]>(response);
        }

        public async Task<FlickrPhotos> GetPhotosForRangeAsync(DateTime? fromDate = null, DateTime? toDate = null, int perPage = 20, int page = 1)
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint, string.Format("/photos/search?min_taken_date={0}&max_taken_date={1}&page={2}&per_page={3}", fromDate ?? new DateTime(2000, 1, 1), fromDate ?? new DateTime(2030, 12, 31), page, perPage)));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<FlickrPhotos>(response);
        }

        public async Task<UploadPhotoResponse> UploadPhotoAsync(UploadPhoto photo)
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint, "/photos"));
            client.Method = "POST";
            client.ContentType = "application/json";

            var dataStream = await client.GetRequestStreamAsync();
            using (var streamWriter = new StreamWriter(dataStream))
            {
                var payload = photo.ToJson();
                streamWriter.Write(payload);
            }

            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<UploadPhotoResponse>(response);
        }

        private static string ExtractBody(WebResponse response)
        {
            var responseStream = response.GetResponseStream();
            using (var reader = new StreamReader(responseStream))
            {
                return reader.ReadToEnd();
            }
        }

        private static TResp ExtractJSonEntity<TResp>(WebResponse response)
        {
            var data = ExtractBody(response);
            return data.FromJson<TResp>();
        }
    }
}
