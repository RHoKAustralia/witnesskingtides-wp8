using System;

namespace KingTides.Core.Api.Models
{
    public class FlickrPhotos
    {
        public FlickrPagePhotos photos { get; set; }
        public string stat { get; set; }
    }

    public class FlickrPagePhotos
    {
        public int page { get; set; }
        public int pages { get; set; }
        public int perpage { get; set; }
        public int total { get; set; }

        public FlickrPhoto[] photo { get; set; }
    }

    public class FlickrPhoto
    {
        public string id { get; set; }

        public string title { get; set; }

        public DateTime datetaken { get; set; }

        public string ownername { get; set; }

        public string originalformat { get; set; }

        public string url_s { get; set; }
        public int height_s { get; set; }
        public int width_s { get; set; }

        public string url_c { get; set; }
        public int height_c { get; set; }
        public int width_c { get; set; }

        public string url_o { get; set; }
        public int height_o { get; set; }
        public int width_o { get; set; }

    }
}