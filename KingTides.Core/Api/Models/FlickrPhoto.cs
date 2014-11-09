using System;
using Newtonsoft.Json;

namespace KingTides.Core.Api.Models
{
    public class FlickrPhotos
    {
        [JsonProperty(PropertyName = "photos")]
        public FlickrPagePhotos Photos { get; set; }
        [JsonProperty(PropertyName = "stat")]
        public string Stat { get; set; }
    }

    public class FlickrPagePhotos
    {
        [JsonProperty(PropertyName = "page")]
        public int Page { get; set; }
        [JsonProperty(PropertyName = "pages")]
        public int Pages { get; set; }
        [JsonProperty(PropertyName = "perpage")]
        public int Perpage { get; set; }
        [JsonProperty(PropertyName = "total")]
        public int Total { get; set; }

        [JsonProperty(PropertyName = "photo")]
        public FlickrPhoto[] Photo { get; set; }
    }

    public class FlickrPhoto
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public decimal Latitude { get; set; }

        [JsonProperty(PropertyName = "longitude")]
        public string Longitude { get; set; }

        [JsonProperty(PropertyName = "datetaken")]
        public DateTime Datetaken { get; set; }

        [JsonProperty(PropertyName = "ownername")]
        public string Ownername { get; set; }

        [JsonProperty(PropertyName = "originalformat")]
        public string Originalformat { get; set; }

        [JsonProperty(PropertyName = "url_s")]
        public string UrlS { get; set; }
        [JsonProperty(PropertyName = "height_s")]
        public int HeightS { get; set; }
        [JsonProperty(PropertyName = "width_s")]
        public int WidthS { get; set; }

        [JsonProperty(PropertyName = "url_c")]
        public string UrlC { get; set; }
        [JsonProperty(PropertyName = "height_c")]
        public int HeightC { get; set; }
        [JsonProperty(PropertyName = "width_c")]
        public int WidthC { get; set; }

        [JsonProperty(PropertyName = "url_o")]
        public string UrlO { get; set; }
        [JsonProperty(PropertyName = "height_o")]
        public int HeightO { get; set; }
        [JsonProperty(PropertyName = "width_o")]
        public int WidthO { get; set; }

    }
}