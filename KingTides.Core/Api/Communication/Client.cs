using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using KingTides.Core.Api.Models;
using KingTides.Core.Extensions;

namespace KingTides.Core.Api.Communication
{
    public class Client
    {
        private readonly string _endpoint;
        private readonly IWebRequestFactory _webRequestFactory;

        public Client(string endpoint, IWebRequestFactory webRequestFactory)
        {
            _endpoint = endpoint;
            _webRequestFactory = webRequestFactory;
        }

        public async Task<KingTideEvent[]> GetKingTideEventsAsync()
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint + "/tide_events"));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<KingTideEvent[]>(response);
        }

        public async Task<KingTideEvent[]> GetFutureKingTideEventsAsync(DateTime? future = null)
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint + string.Format("/tide_events/future/{0}", 
                future.HasValue ? future.Value.ToString("o") : string.Empty)));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<KingTideEvent[]>(response);
        }

        public async Task<KingTideEvent[]> GetCurrentKingTideEventsAsync()
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint + "/tide_events/current"));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<KingTideEvent[]>(response);
        }

        public async Task<Photo[]> GetPhotosForEmailAsync(string email)
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint + "/photos?email=" + email));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<Photo[]>(response);
        }

        public async Task<FlickrPhotos> GetPhotosForRangeAsync(DateTime? fromDate = null, DateTime? toDate = null)
        {
            var client = _webRequestFactory.Create(new Uri(_endpoint + string.Format("/photos/search?min_taken_date={0}&max_taken_date={1}", fromDate ?? new DateTime(2000, 1, 1), fromDate ?? new DateTime(2030, 12, 31))));
            var response = await client.GetResponseAsync();
            return ExtractJSonEntity<FlickrPhotos>(response);
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
